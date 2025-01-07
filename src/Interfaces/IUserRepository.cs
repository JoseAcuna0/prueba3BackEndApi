using System;
using System.Collections.Generic;
using System.Linq;
using ApiPrueba3.src.DTOs;
using System.Threading.Tasks;

namespace ApiPrueba3.src.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> RegisterAsync(RegisterUserDTO registerUser);
        Task<string?> LoginAsync(LoginUserDTO loginUser);
    }
}