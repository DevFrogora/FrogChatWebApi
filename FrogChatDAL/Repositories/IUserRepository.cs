using FrogChatDAL.DomainModel;
using FrogChatModel.DomainModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetUsers();
        Task<IdentityResult> UpdateUser(SignUpUserDto signUpUserDto);
    }
}
