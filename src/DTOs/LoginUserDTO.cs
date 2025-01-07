using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiPrueba3.src.DTOs
{
    public class LoginUserDTO
    {
        [EmailAddress]
        public string email { get; set; } = null!;

        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[0-9]).*$", ErrorMessage = "La contraseña debe contener al menos un número.")]
        public string password { get; set; } = null!;
    }
}