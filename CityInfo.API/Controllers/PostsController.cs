using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ILogger _logger;

        public PostsController(ILogger<UsersController> logger, IMapper mapper, IPostRepository postRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));

        }

        [HttpPost]
        public async Task<IActionResult> AddPost(PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest();
            }
            var finalPost = _mapper.Map<Entities.Post>(postDto);
            //_postRepository.AddPostsForUserAsync(finalPost);



            //if (userExists.Result == true)
            //{
            //    return BadRequest("User already registered");
            //} else
            //{
            //    _userRepository.AddUserAsync(finalUser);
            //}

            return Ok("Sign up request was successful");
        }
    }
}
