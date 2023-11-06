using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UsersController(ILogger<UsersController> logger, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        }

        [HttpPost("signup")]
        public async Task<IActionResult> UserSignup(UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }
            var finalUser = _mapper.Map<Entities.User>(userDto);

            var userExists = _userRepository.UserExistsAsync(userDto.Email);
            _logger.LogInformation($"User Already exists? {userExists.Result}");

            if (userExists.Result == true)
            {
                return BadRequest("User already registered");
            } else
            {
                _userRepository.AddUserAsync(finalUser);
            }

            return Ok("Sign up request was successful");
        }
    }
}
