using anskus.Domain.Models.dbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Data
{
    public interface IDbContext
    {
        DbSet<Cuestionario> Cuestionarios { get; set; }
        DbSet<Pregunta> Preguntas { get; set; }
        DbSet<Respuesta> Respuestas { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
