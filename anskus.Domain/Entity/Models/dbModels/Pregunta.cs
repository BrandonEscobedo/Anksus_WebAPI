using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace anskus.Domain.Models.dbModels;

[Table("preguntas")]
[Index("IdCuestionario", Name = "IX_preguntas_id_cuestionario")]
public partial class Pregunta
{
    [Key]
    [Column("id_pregunta")]
    public int IdPregunta { get; set; }

 
    [ForeignKey("id_cuestionario")]
    public int IdCuestionario { get; set; }

    [Column("pregunta")]
    [StringLength(400)]
    [Unicode(false)]
    public string pregunta { get; set; } = null!;
    [ForeignKey("IdCuestionario")]
    [InverseProperty("Pregunta")]
    public virtual Cuestionario? cuestionario { get; set; }
    [InverseProperty("IdPreguntaNavigation")]
    public virtual ICollection<Respuesta>? Respuesta { get; set; } = new List<Respuesta>();
}
