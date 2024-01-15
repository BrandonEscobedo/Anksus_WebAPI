using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anksus_WebAPI.Models.dbModels;

[Table("ParticipanteEnCuestionario")]
[Index("IdCuestionarioActivo", Name = "IX_ParticipanteEnCuestionario_idCuestionarioActivo")]
[Index("Nombre", Name = "UQ__Particip__72AFBCC655E8C9D6", IsUnique = true)]
public partial class ParticipanteEnCuestionario
{
    [Key]
    [Column("idPeC")]
    public int IdPeC { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("puntos")]
    public int? Puntos { get; set; }

    public int? CantidadPacertadas { get; set; }

    [Column("idCuestionarioActivo")]
    public int IdCuestionarioActivo { get; set; }

    [ForeignKey("IdCuestionarioActivo")]
    [InverseProperty("ParticipanteEnCuestionarios")]
    public virtual CuestionarioActivo IdCuestionarioActivoNavigation { get; set; } = null!;
}
