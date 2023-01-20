using FrogChatDAL.Repositories;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using AutoMapper;

namespace FrogChatWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public AccountController(IAccountRepository accountRepository,IUserRepository userRepository,IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> Signup(SignUpUserDto user)
        {
            var result = await accountRepository.CreateUserAsync(user);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("signin")]
        public async Task<ActionResult> SignIn(SignInDto signInDto)
        {
            var result = await accountRepository.PasswordSignInAsync(signInDto);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(SignInDto signInDto)
        {
            var result = await accountRepository.PasswordSignInAsync(signInDto);
            if (string.IsNullOrEmpty(result))
            {

                return BadRequest("Invalid Credentials");
            }

            var claims = new List<Claim>
            {
                new Claim("Identifier", signInDto.Identifier.ToString()),
                new Claim(ClaimTypes.Email, signInDto.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return Ok("Success");
        }

        [Route("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Ok("success");
        }





        [HttpGet("google-login")]
        [AllowAnonymous]
        public async Task GoogleSignIn(string returnUrl)
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(GoogleLoginCallBack), new { returnUrl })
                });
        }  //https://localhost:7206/

        [HttpGet]
        [Route("google-login-callback")]
        public async Task<IActionResult> GoogleLoginCallBack(string returnURL)
        {
            var authenticationResult = await HttpContext
            .AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (authenticationResult.Succeeded)
            {
                string email = HttpContext
                .User.Claims.Where(_ => _.Type == ClaimTypes.Email)
                .Select(_ => _.Value)
                .FirstOrDefault();

                string firstName = HttpContext
                .User.Claims.Where(_ => _.Type == ClaimTypes.GivenName)
                .Select(_ => _.Value)
                .FirstOrDefault();

                string lastName = HttpContext
                .User.Claims.Where(_ => _.Type == ClaimTypes.Surname)
                .Select(_ => _.Value)
                .FirstOrDefault();

                var user = await ManageExternalLoginUser(
                    new SignUpUserDto()
                    {
                        Email = email,
                        Name = $"{firstName} {lastName}"
                    }
                );

                await RefreshExternalSignIn(user);
                return Redirect($"{returnURL}?externalauth=true");
            }
            return Redirect($"{returnURL}?externalauth=false");
        }

        private async Task<UserDto> ManageExternalLoginUser(SignUpUserDto signUpUserDto)
        {
            var foundUser = await userRepository.GetUser(signUpUserDto.Email.Split("@gmail.com")[0]);
            var user = mapper.Map<UserDto>(foundUser);
            if (user != null)
            {
                return user;
            }
            await accountRepository.CreateUserAsync(signUpUserDto);
            return user;
        }

        private async Task RefreshExternalSignIn(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            HttpContext.User.AddIdentity(claimsIdentity);

            await HttpContext.SignOutAsync();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
