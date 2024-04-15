using System;
using System.Collections.Generic;
using anskus.Application.Data;
using anskus.Domain.Entity.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Anksus_WebAPI.Models.dbModels;

public partial class TestAnskusContext : IdentityDbContext<ApplicationUser>
{
    public TestAnskusContext(DbContextOptions<TestAnskusContext> options)
        : base(options)
    {
    }
    public virtual DbSet<RefreshToken> RefreshToken { get; set; }
    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Cuestionario> Cuestionarios { get; set; }

    public virtual DbSet<CuestionarioActivo> CuestionarioActivos { get; set; }

    public virtual DbSet<ImagenesPerfil> ImagenesPerfils { get; set; } = null!;
    public virtual DbSet<Pregunta> Preguntas { get; set; }

    public virtual DbSet<Respuesta> Respuestas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__categori__8A3D240C8F6227BC");
        });

        modelBuilder.Entity<Cuestionario>(entity =>
        {
            entity.HasKey(e => e.IdCuestionario).HasName("PK__cuestion__4A5CFD1B822640FC");

            entity.Property(e => e.Titulo).HasDefaultValue("title");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Cuestionarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cuestionario_categoria");


        });

        modelBuilder.Entity<CuestionarioActivo>(entity =>
        {
            entity.Property(e => e.IdCuestionario).ValueGeneratedNever();

            entity.HasOne(d => d.IdCuestionarioNavigation).WithOne(p => p.CuestionarioActivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CuestionarioA_cuestionario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.CuestionarioActivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cuestionarioActivo_usuarios");
        });

        modelBuilder.Entity<ImagenesPerfil>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__imagenes__27CC26894DE81741");

            entity.Property(e => e.IdImagen).ValueGeneratedNever();
        });


        modelBuilder.Entity<Pregunta>(entity =>
        {
            entity.HasKey(e => e.IdPregunta).HasName("PK__pregunta__6867FFA45AFDA0E8");

            entity.HasOne(d => d.IdCuestionarioNavigation).WithMany(p => p.Pregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_preguntas_cuestionario");
        });

        modelBuilder.Entity<Respuesta>(entity =>
        {
            entity.HasKey(e => e.IdRespuesta).HasName("PK__respuest__14E55589836F7548");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.Respuesta).HasConstraintName("FK_respuesta_pregunta");
        });


        OnModelCreatingPartial(modelBuilder);



    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


