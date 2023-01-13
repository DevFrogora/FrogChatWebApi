using FrogChatModel.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrogChatWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var user = new DTOUser()
            {
                Email = "my4lol78695@gmail.com",
                Identifier = "109111229606383522361",
                Name = "nanu naka",
                PhotoPath = "https://lh3.googleusercontent.com/a/AEdFTp5jcsydwm4AsQRoEruEyjnu9ic2B8vX1wc3zBC7=s96-c",
            };
            return Ok(user);
        }
    }

    public class DTOUser
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
    }
}
