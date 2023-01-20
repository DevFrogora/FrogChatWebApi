using AutoMapper;
using FrogChatDAL.Repositories;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace FrogChatWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly HttpClient httpClient;

        public UserController(IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("user-profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDto>> UserProfileAsync()
        {
            string userEmail = HttpContext.User.Claims
            .Where(_ => _.Type == ClaimTypes.Email)
            .Select(_ => (_.Value))
            .First();

            var userProfile = await userRepository.GetUser(userEmail.Split("@gmail.com")[0]);
            var user = mapper.Map<UserDto>(userProfile);
            return Ok(user);
        }




        [HttpGet("test")]
        [AllowAnonymous]
        public async Task<ActionResult<Dictionary<string, string>>> GetCurrentUser()
        {
            Dictionary<string, string> claims = new Dictionary<string, string>();
            foreach (var item in HttpContext.User.Claims)
            {
                claims.Add(item.Type, item.Value);
            }
            //return Ok(claims);
            //UserDto user = new();
            //if (User.Identity.IsAuthenticated)
            //{
            //    user.Email = (User.FindFirstValue(User.Identity.Name));
            //}
            return await Task.FromResult(claims);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetUsers()
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
        //[Authorize(Roles = "User")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateUser(UserDto userDto)
        {
            //if (signUpUserDto.Email.Split("@gmail.com")[0].Equals(HttpContext.User.Identity.Name))
            //{
            var result = await userRepository.UpdateUser(userDto);
            if (result == null) return BadRequest();
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
            //    return BadRequest(result.Errors);
            //}
            //else
            //{
            //    return Unauthorized();
            //}

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
