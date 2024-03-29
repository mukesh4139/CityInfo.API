﻿using AutoMapper;
using CityInfo.API.AuthorizationRequirements;
using CityInfo.API.Entities;
using CityInfo.API.Helpers;
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
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger _logger;

        public PostsController(ILogger<UsersController> logger, IMapper mapper, IPostRepository postRepository, IAuthorizationService authorizationService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));

        }

        [HttpPost]
        public async Task<IActionResult> AddPost(PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest();
            }
            var finalPost = _mapper.Map<Entities.Post>(postDto);
            await _postRepository.AddPostsForUserAsync(User.GetCurrentUserId(), finalPost);
            return Ok("Post was saved successfully");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
        {
            var posts = await _postRepository
                 .GetPostsForUserAsync(User.GetCurrentUserId());

            return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
        }

        [HttpGet("{postId}")]
        //[Authorize(Policy = "UserAndPostBelongToSameOrganization")]
        public async Task<ActionResult<PostDto>> GetPost(int postId)
        {
            var post = await _postRepository.GetPostById(postId, true);

            if (post == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, CrudOperationRequirements.ReadRequirement);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(_mapper.Map<PostDto>(post));
        }
    }
}
