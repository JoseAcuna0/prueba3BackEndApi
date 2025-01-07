using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba3.src.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPrueba3.src.Data
{
    public class ApplicationDBContext : DbContext
    {
        // Constructor válido
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        // DbSets para las entidades
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;

        // Configuración adicional de las relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}