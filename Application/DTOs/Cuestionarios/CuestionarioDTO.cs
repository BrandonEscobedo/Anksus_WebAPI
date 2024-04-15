using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace anskus.Application.DTOs.Cuestionarios
{
    public class CuestionarioDTO
    {
       
        [Column("id_cuestionario")]
        public int IdCuestionario { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        public int IdCategoria { get; set; }

        [Column("estado")]
        public bool Estado { get; set; }=false;

        [Column("titulo")]
        [StringLength(60)]
        //[Required(ErrorMessage ="El campo {0} es requerido.")]
        public string? Titulo { get; set; }
        public string? Email { get; set; }

        [Column("publico")]
        public bool Publico { get; set; } = false;

        public virtual ICollection<PreguntasDTO> Pregunta { get; set; } = new List<PreguntasDTO>();

        public static implicit operator CuestionarioDTO(string v)
        {
            throw new NotImplementedException();
        }
    }
}
