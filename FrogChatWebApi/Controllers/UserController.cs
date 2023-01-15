using AutoMapper;
using FrogChatDAL.Repositories;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
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
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IRoleRepository roleRepository,IUserRepository userRepository,IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public  ActionResult Get()
        {
            var users = mapper.Map<IEnumerable<UserDto>>(userRepository.GetUsers());
            return Ok(users);
        }

        [HttpPut("{userName}/roles/{roleName}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> AddRole(string userName, string roleName)
        {
            var result = await roleRepository.AddUserRoleAsync(userName, roleName);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{userName}/roles/{roleName}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> RemoveUserRole(string userName, string roleName)
        {
            var result = await roleRepository.RemoveUserRoleAsync(userName, roleName);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
        }
    }

}
