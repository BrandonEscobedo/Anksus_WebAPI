using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anksus_WebAPI.Models.dbModels;

[Table("categorias")]
public partial class Categoria
{
    [Key]
    [Column("idCategoria")]
    public int IdCategoria { get; set; }

    [Column("categoria")]
    [StringLength(100)]
    public string Categoria1 { get; set; } = null!;

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Cuestionario> Cuestionarios { get; set; } = new List<Cuestionario>();
}
