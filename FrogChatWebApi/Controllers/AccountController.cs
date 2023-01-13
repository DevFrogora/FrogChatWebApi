using FrogChatDAL.Repositories;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrogChatWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;

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
    }
}
