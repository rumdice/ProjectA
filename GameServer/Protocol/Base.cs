namespace Protocol
{
    public abstract class BaseRequset
    {
        public int SessionId { get; set; }
    }

    public abstract class BaseResponse
    {
        public ErrorCode Error { get; set; }
    }
}