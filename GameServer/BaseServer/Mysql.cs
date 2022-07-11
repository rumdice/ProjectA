using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Protocol;
using System.Data.Common;

namespace ServerLib
{

    public class MySqlConfig
    {
        public string host;
        public int port;
        public string uid;
        public string pwd;
        public string database;
    }

    public class MySql
    {
        private static MySqlConfig config;
        private static string connectString;

        public MySql()
        {
            var file = Config.LoadConfig("MysqlConfig.json");
            config = JsonConvert.DeserializeObject<MySqlConfig>(file);

            connectString = @$"
                server = {config.host};
                port = {config.port};
                uid = {config.uid};
                pwd = {config.pwd};
                database = {config.database};
                Pooling = true;
                MIN Pool Size = 5;
                Max Pool Size = 10;
                Connection Timeout = 60;
                allow user variables = true;
                Allow Zero Datetime = true;
                Convert Zero DateTime = true;
                ";
        }

        public static async Task<DbDataReader> ExecuteReaderAsync(string query)
        {
            try
            {
                using (var conn = new MySqlConnection(connectString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        var dbResult = await cmd.ExecuteReaderAsync();
                        var dt = new System.Data.DataTable();
                        dt.Load(dbResult);
                        return dt.CreateDataReader();
                    }
                }
            }
            catch
            {
                throw new Error(ErrorCode.QUERY_ERROR);
            }
        }

        public static async Task<object> ExecuteScalorAsync(string query)
        {
            try
            {
                using (var conn = new MySqlConnection(connectString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new MySqlCommand(query, conn))
                        return await cmd.ExecuteScalarAsync();
                }
            }
            catch
            {
                throw new Error(ErrorCode.QUERY_ERROR);
            }
        }

        public static async Task ExecuteWriterAsync(string query)
        {
            try
            {
                using (var conn = new MySqlConnection(connectString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new MySqlCommand(query, conn))
                        await cmd.ExecuteNonQueryAsync();
                }
            }
            catch
            {
                throw new Error(ErrorCode.QUERY_ERROR);
            }
        }

        //public static async Task<bool> DoTransactionAsync(List<string> queryList)
        //{
        //    var iter = 0;
        //    var cmd = new MySqlCommand();

        //    using (var trans = await mysqlConnection.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            cmd.Connection = mysqlConnection;
        //            cmd.Transaction = trans;

        //            if (mysqlConnection == null || trans == null)
        //                return false;

        //            for (var i = 0; i < queryList.Count; i++)
        //            {
        //                cmd.CommandText = queryList[i];
        //                iter += await cmd.ExecuteNonQueryAsync();
        //            }

        //            await trans.CommitAsync();
        //            return true;
        //        }
        //        catch
        //        {
        //            await trans.RollbackAsync();
        //            throw new Error(ResultCode.QUERY_FAIL);
        //        }
        //    }
        //}
    }
}
