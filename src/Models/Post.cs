using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ApiPrueba3.src.Models
{
    public class Post
    {   
        public int Id { get; set; }

        [MinLength(5, ErrorMessage = "El título debe tener al menos 5 caracteres.")]
        public required string titulo { get; set; }

        
        public required DateTime publishDate { get; set; } = DateTime.Now;

        [Url(ErrorMessage = "Debe ser una URL válida.")]
        public required string url { get; set; }

        //relacion user y post
        public required int UserId { get; set; } //FK User
        public User User { get; set; } = null!;

        
    }
}