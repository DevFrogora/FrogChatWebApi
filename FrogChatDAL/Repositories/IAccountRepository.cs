using FrogChatModel.DomainModel;
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
    }
}
