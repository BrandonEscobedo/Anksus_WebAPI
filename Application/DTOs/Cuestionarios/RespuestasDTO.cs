using System.ComponentModel.DataAnnotations.Schema;

namespace anskus.Application.DTOs.Cuestionarios
{
    public partial class RespuestasDTO
    {  
        public int IdRespuesta { get; set; }  
        public int IdPregunta { get; set; }
        public string? ClaseCSS { get; set; } = "";

        public string respuesta { get; set; } = null!;

        [Column("R_correcta")]
        public bool RCorrecta { get; set; }

    }
}
