using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Anksus_WebAPI.Models.DTO
{
    public class RespuestasDTO
    {
        [Key]
        [Column("id_respuesta")]
        public int IdRespuesta { get; set; }

        [Column("id_pregunta")]
        public int IdPregunta { get; set; }

        [Column("respuesta")]
        [StringLength(200)]
      
        public string respuesta { get; set; } = null!;

        [Column("R_correcta")]
        public bool RCorrecta { get; set; }

    }
}
