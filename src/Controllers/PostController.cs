using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiPrueba3.src.Interfaces;
using ApiPrueba3.src.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApiPrueba3.src.Controllers
{
    
    [ApiController]
    [Route("api/posts")]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO createPost)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Obtener el ID del usuario autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("Usuario no autenticado.");

            var post = await _postRepository.CreatePostAsync(createPost, userId);
            return CreatedAtAction(nameof(GetPosts), new { id = post.Id }, post);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }
    }
}