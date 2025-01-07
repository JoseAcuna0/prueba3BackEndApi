using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiPrueba3.src.Models
{
    public class User
    {
        public int Id { get; set; }

        [EmailAddress]
        public required string email { get; set; }

        
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[0-9]).*$", ErrorMessage = "La contraseña debe contener al menos un número.")]
        public required string password { get; set; }

         public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}