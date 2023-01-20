using FrogChatDAL.DomainModel;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
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
        Task<ApplicationUser> GetUser(string username);
        Task<IdentityResult> UpdateUser(UserDto signUpUserDto);
        Task<IdentityResult> DeleteUser(string userName);
    }
}
