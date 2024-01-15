﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Anksus_WebAPI.Models.dbModels;

[Table("imagenes_perfil")]
public partial class ImagenesPerfil
{
    [Key]
    [Column("id_imagen")]
    [JsonPropertyName("imagen")]
    public int IdImagen { get; set; }

    [Column("imagen")]
    [StringLength(500)]
    public string Imagen { get; set; } = null!;
    [InverseProperty("IdImagenPerfilNavigation")]
    public virtual ICollection<AplicationUser> Usuarios { get; set; } = new List<AplicationUser>();

}
