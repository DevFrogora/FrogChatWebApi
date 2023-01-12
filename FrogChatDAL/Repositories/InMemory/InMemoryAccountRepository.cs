using FrogChatModel.DomainModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories.InMemory
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly UserManager<IdentityUser> userManager;

        public InMemoryAccountRepository(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;

        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserDto signUpUserDto)
        {
            var user = new IdentityUser()
            {
                UserName = signUpUserDto.Name,
                Email = signUpUserDto.Email,
            };
            return await userManager.CreateAsync(user,signUpUserDto.Identifier+"FrogChat@");

        }
    }
}
