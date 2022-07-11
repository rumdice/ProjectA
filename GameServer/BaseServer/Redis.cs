using Newtonsoft.Json;
using Protocol;
using ServerLib;
using StackExchange.Redis;

namespace GbServices.Database
{
    public class RedisConfig
    {
        public string host;
        public int port;
        public int db;
        public bool cluster;
    }

    public class Redis
    {
        public static ConnectionMultiplexer redisConnection;
        public static IDatabase redis;
        private static RedisConfig config;
        private readonly string connectString;

        public Redis()
        {
            var file = Config.LoadConfig("RedisConfig.json");
            config = JsonConvert.DeserializeObject<RedisConfig>(file);

            this.connectString = @$"{config.host}:{config.port},abortConnect=false";
        }

        public bool Connect()
        {
            try
            {
                redisConnection = ConnectionMultiplexer.Connect(connectString);
                if (redisConnection != null)
                {
                    redis = redisConnection.GetDatabase();
                }
                return true;
            }
            catch
            {
                throw new Error(ErrorCode.REDIS_ERROR);
            }
        }

        public void DisConnect()
        {
            redisConnection.Dispose();
        }

        public static string StringGet(string key)
        {
            return redis.StringGet(key);
        }

        public static bool StringSet(string key, string val, long tick = 0)
        {
            var ttl = TimeSpan.FromSeconds(tick);
            return (tick != 0) ? redis.StringSet(key, val, ttl) : redis.StringSet(key, val);
        }

        public static async Task<string> StringGetAsync(string key)
        {
            return await redis.StringGetAsync(key);
        }

        public static async Task<bool> StringSetAsync(string key, string val, long tick = 0)
        {
            var ttl = TimeSpan.FromSeconds(tick);

            if (tick != 0)
                return await redis.StringSetAsync(key, val, ttl);
            else
                return await redis.StringSetAsync(key, val);
        }

        public static RedisValue HashGet(string key, RedisValue field)
        {
            return redis.HashGet(key, field);
        }

        public static bool HashSet(string key, RedisValue field, RedisValue val)
        {
            return redis.HashSet(key, field, val);
        }

        public static async Task<RedisValue> HashGetAsync(string key, RedisValue field)
        {
            return await redis.HashGetAsync(key, field);
        }

        public static async void HashSetAsync(string key, HashEntry[] value)
        {
            await redis.HashSetAsync(key, value);
        }

        public static string HashVal(string key, string sessionID)
        {
            var resultList = redis.HashGetAll(key);
            foreach (var result in resultList)
            {
                if (Convert.ToString(result.Value) == sessionID)
                {
                    var n = result.Name;
                    return n;
                }
            }

            return "";
        }


        public static void KeyDelete(string key)
        {
            redis.KeyDelete(key);
        }

        public static void Expire(string key, long tick = 0)
        {
            var ttl = TimeSpan.FromSeconds(tick);
            redis.KeyExpire(key, ttl);
        }
    }
}
