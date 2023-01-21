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
using Microsoft.AspNetCore.Http.HttpResults;

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

        [AllowAnonymous]
        [HttpGet("google-login")]
        public async Task GoogleSignIn( string returnURL)
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(GoogleLoginCallBack), new { returnURL })
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

                string Name = HttpContext
                .User.Claims.Where(_ => _.Type == ClaimTypes.Name)
                .Select(_ => _.Value)
                .FirstOrDefault();

                string identifier = HttpContext
                .User.Claims.Where(_ => _.Type == ClaimTypes.NameIdentifier)
                .Select(_ => _.Value)
                .FirstOrDefault();

                string picture = HttpContext
                .User.Claims.Where(_ => _.Type == "picture")
                .Select(_ => _.Value)
                .FirstOrDefault();

                var signup = new SignUpUserDto()
                {
                    Identifier = identifier,
                    Email = email,
                    Name = Name,
                    PhotoPath = picture
                };
                var user = await ManageExternalLoginUser(
                    signup
                );
                SignInDto signInDto = new SignInDto()
                {
                    Email = email,
                    Identifier = identifier
                };

                var token = await SignIn(signInDto);
                if (token is UnauthorizedResult)
                {
                    return Redirect($"{returnURL}?access_token=UnAuthenticated");
                }
                else
                {
                    var result3 = (OkObjectResult)token.Result;
                    return Redirect($"{returnURL}?access_token={result3.Value}");
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
