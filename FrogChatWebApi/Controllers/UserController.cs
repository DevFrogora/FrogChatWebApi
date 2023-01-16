using AutoMapper;
using FrogChatDAL.Repositories;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("Test")]
        public ActionResult Test()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                var email = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                return Ok(email);
            }
            return NotFound(null);
        }


        [HttpGet]
        public  ActionResult GetUsers()
        {
            var users = mapper.Map<IEnumerable<UserDto>>(userRepository.GetUsers());
            return Ok(users);
        }

        [HttpPut("{userName}/roles/{roleName}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> AddUserRole(string userName, string roleName)
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

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> UpdateUser(SignUpUserDto signUpUserDto)
        {
            //User.Claims
            var result = await userRepository.UpdateUser(signUpUserDto);
            if (result == null) return BadRequest();
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{userName}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> DeleteUser(string userName)
        {
            //User.Claims
            var result = await userRepository.DeleteUser(userName);
            if (result == null) return BadRequest();
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
        }

    }

}
