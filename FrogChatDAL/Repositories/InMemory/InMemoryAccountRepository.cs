using FrogChatDAL.DomainModel;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public InMemoryAccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserDto signUpUserDto)
        {
            //signUpUserDto.Name = signUpUserDto.Name.Trim().Replace(" ", "");
            var user = new ApplicationUser()
            {
                UserName = signUpUserDto.Email.Split("@gmail.com")[0],
                Email = signUpUserDto.Email,
                Name = signUpUserDto.Name,
                PhotoUrl = signUpUserDto.PhotoPath,
            };
            return await userManager.CreateAsync(user,signUpUserDto.Identifier+"FrogChat@");

        }

        public async Task<SignInResult> PasswordSignInAsync(SignInDto signInDto)
        {
            return  await  signInManager.PasswordSignInAsync(signInDto.Email,
                signInDto.Identifier + "FrogChat@", signInDto.RememberMe, false);
        }
    }
}
