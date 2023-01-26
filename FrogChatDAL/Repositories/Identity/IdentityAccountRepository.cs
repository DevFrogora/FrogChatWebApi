using FrogChatDAL.DomainModel;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories.Identity
{
    public class IdentityAccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public IdentityAccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
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

            var result = await userManager.CreateAsync(user, signUpUserDto.Identifier + "FrogChat@");
            if (!result.Succeeded)
            {
                return result;
            }
            await userManager.AddToRoleAsync(await userManager.FindByEmailAsync(user.Email), "User");
            return result;
        }

        public async Task<string> PasswordSignInAsync(SignInDto signInDto)
        {
            var user = await userManager.FindByEmailAsync(signInDto.Email);
            if (user == null) return null;
            var result = await signInManager.PasswordSignInAsync(user.UserName,
                signInDto.Identifier + "FrogChat@", signInDto.RememberMe, false);

            if (!result.Succeeded) return null;

            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("picture",user.PhotoUrl),
                new Claim("userId",user.Id),
                new Claim("username",user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:key")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                audience: configuration.GetValue<string>("Jwt:Audience"),
                expires: DateTime.UtcNow.AddDays(1),
                claims: authClaims,
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
