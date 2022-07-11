using Protocol;

namespace ServerLib
{
    public class Error : Exception
    {
        public readonly ErrorCode code;
        public Error(ErrorCode errorCode)
        {
            code = errorCode;
        }
    }
}
