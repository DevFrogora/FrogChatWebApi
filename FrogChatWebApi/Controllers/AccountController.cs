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
        public async Task<ActionResult<string>> SignIn(SignInDto signInDto)
        {
            var token = await accountRepository.PasswordSignInAsync(signInDto);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Ok(token);
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
        public async Task<ActionResult> GoogleLoginCallBack(string returnURL)
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


                var signup = new SignUpUserDto()
                {
                    Email = email,
                    Name = $"{firstName} {lastName}"
                };
                var user = await ManageExternalLoginUser(
                    signup
                );
                SignInDto signInDto = new SignInDto()
                {
                    Email = email,
                    Identifier = $"{firstName} {lastName}"
                };

                var token = await SignIn(signInDto);
                if(token is UnauthorizedResult)
                {
                    return Redirect($"{returnURL}?access_token=UnAuthenticated");
                }
                else
                {
                    return Redirect($"{returnURL}?access_token={token.Value}");
                }
            }
            return Redirect($"{returnURL}?access_token=UnAuthenticated");
        }

        private async Task<UserDto> ManageExternalLoginUser(SignUpUserDto signUpUserDto)
        {
            var foundUser = await userRepository.GetUser(signUpUserDto.Email.Split("@gmail.com")[0]);
            if (foundUser != null)
            {
                var user = mapper.Map<UserDto>(foundUser);
                return user;
            }
            await Signup(signUpUserDto);
            return mapper.Map<UserDto>(signUpUserDto);
        }

    }
}
