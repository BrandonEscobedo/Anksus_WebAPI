using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace anskus.Domain.Models.dbModels;

[Table("imagenes_perfil")]
public partial class ImagenesPerfil
{
    [Key]
    [Column("id_imagen")]
    public int IdImagen { get; set; }

    [Column("imagen")]
    [StringLength(500)]
    public string Imagen { get; set; } = null!;


}
