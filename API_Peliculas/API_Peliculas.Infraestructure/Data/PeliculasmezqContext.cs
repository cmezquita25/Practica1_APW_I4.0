using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using API_Peliculas.Domain.Entities;

#nullable disable

namespace API_Peliculas.Infraestructure.Data
{
    public partial class PeliculasmezqContext : DbContext
    {
        public PeliculasmezqContext()
        {
        }

        public PeliculasmezqContext(DbContextOptions<PeliculasmezqContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pelicula> Peliculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_IDPELICULAS");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Director)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaPublicacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Genero)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
