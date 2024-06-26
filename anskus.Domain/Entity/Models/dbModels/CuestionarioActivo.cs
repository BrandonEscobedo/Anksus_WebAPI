﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using anskus.Domain.Authentication;
using Microsoft.EntityFrameworkCore;

namespace anskus.Domain.Models.dbModels;

[Table("cuestionarioActivo")]
[Index("Codigo", Name = "IX_cuestionarioActivo", IsUnique = true)]
public partial class CuestionarioActivo
{
    [Key]
    [Column("id_cuestionario")]
    public int IdCuestionario { get; set; }

    public int Codigo { get; set; }
    [NotMapped]
    public Cuestionario? cuestionario { get; set; } 
    public string? IdUsuario { get; set; }
    [ForeignKey("IdUsuario")]
    [InverseProperty("CuestionarioActivo")] 
    public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;
    [ForeignKey("IdCuestionario")]
    [InverseProperty("CuestionarioActivo")]
    public virtual Cuestionario IdCuestionarioNavigation { get; set; } = null!;

}
