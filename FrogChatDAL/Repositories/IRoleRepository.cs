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
        Task<IdentityResult> CreatRole(RoleDto roleDto);
        IEnumerable<IdentityRole> GetRoles();

    }
}
