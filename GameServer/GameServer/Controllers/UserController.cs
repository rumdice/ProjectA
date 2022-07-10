using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Protocol;

namespace GameServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("Signup")]
        public async Task<LoginResponse> Signup(LoginRequest request)
        {
            return null;
        }
    }
}
