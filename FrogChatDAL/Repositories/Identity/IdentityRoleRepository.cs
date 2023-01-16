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

        public async Task<IdentityResult> AddUserRoleAsync(string userName, string roleName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }
            return await userManager.AddToRoleAsync(user, roleName.ToUpper());
        }

        public async Task<IdentityResult> RemoveUserRoleAsync(string userName, string roleName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }
            return await userManager.RemoveFromRoleAsync(user,roleName);
        }


        public async Task<IdentityResult> CreatRole([Required] AddRoleDto roleDto)
        {
            return await roleManager.CreateAsync(new IdentityRole(roleDto.Name));
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            return roleManager.Roles.ToList();
        }

        public async Task<IdentityResult> UpdateRole(UpdateRoleDto roleDto)
        {
            var role = await roleManager.FindByIdAsync(roleDto.Id);
            if(role == null)
            {
                return null;
            }
            role.Name = roleDto.Name;
            return await roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteRole(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null) return null;
            return await roleManager.DeleteAsync(role);
        }
    }
}
