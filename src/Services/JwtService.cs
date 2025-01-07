using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ApiPrueba3.src.Services
{
    public class JwtService
    {
        public string GenerateToken(IdentityUser user)
        {
            if (string.IsNullOrEmpty(user.UserName))
                throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío.", nameof(user.UserName));

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("El email no puede ser nulo o vacío.", nameof(user.Email));

            // Obtener las configuraciones desde las variables de entorno
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var jwtExpirationHours = Environment.GetEnvironmentVariable("JWT_EXPIRATION_HOURS") ?? "3";

            if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
            {
                throw new InvalidOperationException("Las variables de entorno JWT_KEY, JWT_ISSUER o JWT_AUDIENCE no están configuradas.");
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                expires: DateTime.Now.AddHours(double.Parse(jwtExpirationHours)),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
