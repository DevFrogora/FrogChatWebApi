using AutoMapper;
using FrogChatDAL.Repositories;
using FrogChatModel.DomainModel;
using Microsoft.AspNetCore.Mvc;

namespace FrogChatWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                return Ok(await userRepository.GetUsersAsync());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(string id)
        {
            try
            {
                return Ok(await userRepository.GetUserAsync(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(DTOUser user)
        {
            try
            {
                var dest = mapper.Map<DTOUser, TblUser>(user);
                dest.Role = new TblRole { Id = 3, Name = "User" };

                var newUser = await userRepository.AddUserAsync(dest);
                var newUserDTO = mapper.Map<TblUser, DTOUser>(newUser);

                return CreatedAtAction(nameof(GetUser), new { id = newUserDTO.Identifier }, newUserDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database");
            }
        }
    }
}