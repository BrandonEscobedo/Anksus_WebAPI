using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anksus_WebAPI.Models.dbModels;

[Table("cuestionarioActivo")]
[Index("Codigo", Name = "IX_cuestionarioActivo", IsUnique = true)]
[Index("IdUsuario", Name = "IX_cuestionarioActivo_1", IsUnique = true)]
public partial class CuestionarioActivo
{
    [Key]
    [Column("id_cuestionario")]
    public int IdCuestionario { get; set; }

    public int Codigo { get; set; }

    public int IdUsuario { get; set; }

    [ForeignKey("IdCuestionario")]
    [InverseProperty("CuestionarioActivo")]
    public virtual Cuestionario IdCuestionarioNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("CuestionarioActivo")]
    public virtual AplicationUser IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdCuestionarioActivoNavigation")]
    public virtual ICollection<ParticipanteEnCuestionario> ParticipanteEnCuestionarios { get; set; } = new List<ParticipanteEnCuestionario>();
}
