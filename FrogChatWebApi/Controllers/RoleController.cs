using FrogChatDAL.Repositories;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrogChatWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpGet]
        [Authorize]
        public  ActionResult GetRoles()
        {
            return Ok(roleRepository.GetRoles());
        }

        [HttpPost]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<ActionResult> CreatRole(AddRoleDto roleDto)
        {

            var result = await roleRepository.CreatRole(roleDto);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> UpdateRole(UpdateRoleDto roleDto)
        {
            var result = await roleRepository.UpdateRole(roleDto);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
        }
    }
}
