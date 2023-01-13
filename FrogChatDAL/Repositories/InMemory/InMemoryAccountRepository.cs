using FrogChatDAL.DomainModel;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories.InMemory
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public InMemoryAccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager , IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
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

        public async Task<string> PasswordSignInAsync(SignInDto signInDto)
        {
            var result=   await  signInManager.PasswordSignInAsync(signInDto.Email,
                signInDto.Identifier + "FrogChat@", signInDto.RememberMe, false);

            if (!result.Succeeded)
            {
                return null;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , signInDto.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSiginKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims : authClaims,
                signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
