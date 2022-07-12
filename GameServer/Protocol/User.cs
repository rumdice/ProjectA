namespace Protocol
{
    public class LoginRequest
    {
        public long UserId { get; set; }
    }

    public class LoginResponse : BaseResponse
    {
        public int SessionId { get; set; }
    }
}