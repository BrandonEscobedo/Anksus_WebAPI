using System;
using System.Collections.Generic;
using anskus.Application.Data;
using anskus.Domain;
using anskus.Domain.Authentication;
using anskus.Domain.Entity.Authentication;
using anskus.Domain.Entity.Tipados;
using anskus.Domain.Models.dbModels;
using anskus.Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace anskus.Infrastructure.Data;

public partial class TestAnskusContext : IdentityDbContext<ApplicationUser>, IDbContext,IUnitOfWork
{
    private readonly IPublisher _publisher;
    public TestAnskusContext(DbContextOptions<TestAnskusContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher ?? throw new ArgumentException(nameof(_publisher));
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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestAnskusContext).Assembly);
      
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__categori__8A3D240C8F6227BC");
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
        modelBuilder.Entity<Cuestionario>(e =>
        {
            e.HasKey(e => e.IdCuestionario).HasName("PK__cuestion__4A5CFD1B822640FC");
            e.HasOne(x=>x.IdCategoriaNavigation).WithMany(p=> p.Cuestionarios)
            .HasConstraintName("FK_cuestionario_categoria");
            e.Property(e => e.Titulo).HasDefaultValue("title")
                  .HasMaxLength(60);
            e.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Cuestionario)
            .HasConstraintName("FK_cuestionario_usuario");
           
    //        e.Property(e => e.IdCuestionario).HasConversion(
    //cuestionarioId => cuestionarioId.Value,
    //valor => new CuestionarioId(valor));
          

        });
        modelBuilder.Entity<ImagenesPerfil>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__imagenes__27CC26894DE81741");

            entity.Property(e => e.IdImagen).ValueGeneratedNever();
        });


        modelBuilder.Entity<Pregunta>(entity =>
        {
            entity.HasKey(e => e.IdPregunta).HasName("PK__pregunta__6867FFA45AFDA0E8");
            entity.HasOne(x => x.cuestionario)
            .WithMany(x => x.Pregunta)
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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {

        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany(x => x.GetDomainEvents());
        var result = await base.SaveChangesAsync(cancellationToken);
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }
        return result;
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


