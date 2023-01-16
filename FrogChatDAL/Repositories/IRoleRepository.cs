using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories
{
    public interface IRoleRepository
    {
        Task<IdentityResult> CreatRole(AddRoleDto roleDto);
        IEnumerable<IdentityRole> GetRoles();
        Task<IdentityResult> UpdateRole(UpdateRoleDto roleDto);
        Task<IdentityResult> AddUserRoleAsync(string userName, string roleName);
        Task<IdentityResult> RemoveUserRoleAsync(string userName, string roleName);
        Task<IdentityResult> DeleteRole(string roleName);

    }
}
