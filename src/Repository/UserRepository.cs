using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba3.src.Interfaces;
using ApiPrueba3.src.DTOs;
using Microsoft.AspNetCore.Identity;
using ApiPrueba3.src.Services;

namespace ApiPrueba3.src.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtService;

        public UserRepository(UserManager<IdentityUser> userManager, JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

         public async Task<bool> RegisterAsync(RegisterUserDTO registerUser)
        {
            var user = new IdentityUser { UserName = registerUser.email, Email = registerUser.email };
            var result = await _userManager.CreateAsync(user, registerUser.password);

            return result.Succeeded;
        }

        public async Task<string?> LoginAsync(LoginUserDTO loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUser.password))
                return null;

            // Generar y retornar el token JWT
            return _jwtService.GenerateToken(user);
        }
    }
}