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
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserDto signUpUserDto);
        Task<string> PasswordSignInAsync(SignInDto signInDto);
    }
}
