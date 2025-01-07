using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba3.src.DTOs;

namespace ApiPrueba3.src.Interfaces
{
    public interface IPostRepository
    {
        Task<PostDTO> CreatePostAsync(CreatePostDTO createPost, string userId);
        Task<IEnumerable<PostDTO>> GetAllPostsAsync();
    }
}