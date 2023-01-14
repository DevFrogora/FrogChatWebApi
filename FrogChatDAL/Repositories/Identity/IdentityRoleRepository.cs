using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories.Identity
{
    public class IdentityRoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> roleManager;


        public IdentityRoleRepository(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> CreatRole([Required] RoleDto roleDto)
        {
            return await roleManager.CreateAsync(new IdentityRole(roleDto.Name));
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            return roleManager.Roles.ToList();
        }

        public async Task<IdentityResult> UpdateRole(RoleDto roleDto)
        {
            var role = await roleManager.FindByIdAsync(roleDto.Id);
            if(role == null)
            {
                return null;
            }
            return await roleManager.UpdateAsync(role);
        }
    }
}
