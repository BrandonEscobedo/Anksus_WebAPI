﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace anskus.Application.DTOs.Cuestionarios
{
    public class PreguntasDTO
    {
        [Key]
        [Column("id_pregunta")]
        public int IdPregunta { get; set; }

        [Column("id_cuestionario")]
        public int IdCuestionario { get; set; }

        [Column("pregunta")]
        [StringLength(400)]
        [Required(ErrorMessage ="Este campo Es Requerido.")]
        public string Pregunta { get; set; } = null!;

        [Column("estado")]
        public bool? Estado { get; set; }

        public virtual ICollection<RespuestasDTO> Respuesta { get; set; } = new List<RespuestasDTO>();
    }
}
