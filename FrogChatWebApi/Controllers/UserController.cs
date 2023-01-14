using FrogChatDAL.Repositories;
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
        private readonly IRoleRepository roleRepository;

        public UserController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

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

        [HttpPut("{userEmail}/roles/{roleName}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> AddRole(string userEmail, string roleName)
        {
            var result = await roleRepository.AddUserRoleAsync(userEmail, roleName);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{userEmail}/roles/{roleName}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> RemoveUserRole(string userEmail, string roleName)
        {
            var result = await roleRepository.AddUserRoleAsync(userEmail, roleName);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
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
