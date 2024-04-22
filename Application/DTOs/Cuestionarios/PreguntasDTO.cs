
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace anskus.Application.DTOs.Cuestionarios
{
    public class PreguntasDTO
    { 
        public int IdPregunta { get; set; }
        public int IdCuestionario { get; set; }

        public string pregunta { get; set; } = null!;
        public virtual CuestionarioDTO? cuestionario { get; set; }

        public virtual ICollection<RespuestasDTO>? Respuesta { get; set; } = new List<RespuestasDTO>();
    }
}
