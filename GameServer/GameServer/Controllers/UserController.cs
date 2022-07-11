using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Protocol;

namespace GameServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return null;
        }
    }
}
