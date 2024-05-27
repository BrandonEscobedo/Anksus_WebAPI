using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using anskus.Domain.Models.dbModels;

namespace anskus.Application.DTOs.Cuestionarios
{
    public partial class CuestionarioDTO
    {
       public int IdCuestionario { get; set; }

        public string? IdUsuario { get; set; }

        public int IdCategoria { get; set; }

        public string? Titulo { get; set; }

        public bool Publico { get; set; }
        public virtual ICollection<PreguntasDTO> Pregunta { get; set; } = new List<PreguntasDTO>();

    }
}
