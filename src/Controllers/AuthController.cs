using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiPrueba3.src.Interfaces;
using ApiPrueba3.src.DTOs;

namespace ApiPrueba3.src.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userRepository.RegisterAsync(registerUser);
            if (!result)
                return BadRequest("Error al registrar el usuario. El correo puede estar en uso.");

            return Ok("Usuario registrado exitosamente.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _userRepository.LoginAsync(loginUser);
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Credenciales incorrectas.");

            return Ok(new { Token = token });
        }
    }
}