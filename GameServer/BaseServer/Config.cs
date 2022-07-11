using Protocol;

namespace ServerLib
{
    public class Config
    {
        public static string LoadConfig(string configFileName)
        {
            try
            {
                var dir = Directory.GetCurrentDirectory();
                var ConfigPath = Path.Join(dir, $"Config/{configFileName}");
                var file = File.ReadAllText(ConfigPath);
                return file;
            }
            catch
            {
                throw new Error(ErrorCode.INVAILD_ERROR);
            }
        }
    }
}
