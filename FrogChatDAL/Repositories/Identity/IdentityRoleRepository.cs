using FrogChatDAL.DomainModel;
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
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityRoleRepository(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddUserRoleAsync(string userEmail, string roleName)
        {
            var user = await userManager.FindByEmailAsync(addUserRoleDto.UserEmail);
            if (user == null)
            {
                return null;
            }
            return await userManager.AddToRoleAsync(user, addUserRoleDto.RoleName);
        }

        public async Task<IdentityResult> RemoveUserRoleAsync(string userEmail, string roleName)
        {
            var user = await userManager.FindByEmailAsync(addUserRoleDto.UserEmail);
            if (user == null)
            {
                return null;
            }
            return await userManager.RemoveFromRoleAsync(user, addUserRoleDto.RoleName);
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
