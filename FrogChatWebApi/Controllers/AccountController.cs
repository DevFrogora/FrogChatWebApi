using FrogChatDAL.Repositories;
using FrogChatModel.DomainModel;
using Microsoft.AspNetCore.Http;
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
            var result  = await accountRepository.CreateUserAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);

            }
            return Ok();
        }
    }
}
