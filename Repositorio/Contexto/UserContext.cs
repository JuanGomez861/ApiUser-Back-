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

                entity.Property(e => e.IdUsuario).ValueGeneratedOnAdd();


                entity.Property(e => e.Nombre)
                     .HasMaxLength(100)
                     .IsRequired();

                // Definir el apellido como obligatorio y con longitud máxima de 100
                entity.Property(e => e.Apellido)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.HasIndex(e => e.Cedula)
                      .IsUnique();

                entity.Property(e => e.Telefono)
                     .HasMaxLength(20)
                     .IsRequired();

                entity.Property(e => e.Direccion)
                     .HasMaxLength(100)
                     .IsRequired();


                entity.ToTable("user");

            });
        }
    }
}
