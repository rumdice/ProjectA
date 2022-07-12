using System.Text.Json;

namespace Protocol
{
    public class ErrorResponse : BaseResponse
    {
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}