using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anksus_WebAPI.Models.dbModels;

[Table("cuestionarios")]
[Index("IdCategoria", Name = "IX_cuestionarios_IdCategoria")]
[Index("IdUsuario", Name = "IX_cuestionarios_id_usuario")]
public partial class Cuestionario
{
    [Key]
    [Column("id_cuestionario")]
    public int IdCuestionario { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    public int IdCategoria { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("titulo")]
    [StringLength(60)]
    public string? Titulo { get; set; }

    [Column("publico")]
    public bool Publico { get; set; }

    [InverseProperty("IdCuestionarioNavigation")]
    public virtual CuestionarioActivo? CuestionarioActivo { get; set; }

    [ForeignKey("IdCategoria")]
    [InverseProperty("Cuestionarios")]
    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Cuestionarios")]
    public virtual AplicationUser IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdCuestionarioNavigation")]
    public virtual ICollection<Pregunta> Pregunta { get; set; } = new List<Pregunta>();
}
