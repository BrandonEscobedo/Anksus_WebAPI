using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using anskus.Domain.Entity.Tipados;
using anskus.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Anksus_WebAPI.Models.dbModels;
public partial class Cuestionario:AggregateRoot
{

    public int IdCuestionario { get; set; }

    public int IdUsuario { get; set; }

    public int IdCategoria { get; set; }

    public string? Titulo { get; set; }
   
    public bool Publico { get; set; }

    [InverseProperty("IdCuestionarioNavigation")]
    public virtual CuestionarioActivo? CuestionarioActivo { get; set; }
    [ForeignKey("IdCategoria")]
    [InverseProperty("Cuestionarios")]
    public virtual Categoria? IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<Pregunta>? Pregunta { get; set; } = new List<Pregunta>();
}
