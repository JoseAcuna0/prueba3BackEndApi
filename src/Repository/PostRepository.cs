using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba3.src.Interfaces;
using ApiPrueba3.src.DTOs;
using Microsoft.AspNetCore.Identity;
using ApiPrueba3.src.Data;
using Microsoft.EntityFrameworkCore;
using ApiPrueba3.src.Models;


namespace ApiPrueba3.src.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDBContext _context;

        public PostRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<PostDTO> CreatePostAsync(CreatePostDTO createPost, string userId)
        {
            var post = new Post
            {
                title = createPost.title,
                url = createPost.url,
                publishDate = DateTime.Now,
                UserId = int.Parse(userId)
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return new PostDTO
            {
                Id = post.Id,
                title = post.title,
                url = post.url,
                publishDate = post.publishDate
            };
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Select(post => new PostDTO
                {
                    Id = post.Id,
                    title = post.title,
                    url = post.url,
                    publishDate = post.publishDate
                }).ToListAsync();
        }

    }
}