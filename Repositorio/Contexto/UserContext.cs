using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Contexto
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options):base(options) 
        { 
        }

        DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e=>e.IdUsuario);

                entity.Property(e=>e.IdUsuario).ValueGeneratedOnAdd();

                entity.ToTable("user");

            });
        }
    }
}
