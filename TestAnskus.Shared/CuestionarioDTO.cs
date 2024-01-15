using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Anksus_WebAPI.Models.DTO
{
    public class CuestionarioDTO
    {
       
        [Column("id_cuestionario")]
        public int IdCuestionario { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        public int IdCategoria { get; set; }

        [Column("estado")]
        public bool? Estado { get; set; }

        [Column("titulo")]
        [StringLength(60)]
        [Required(ErrorMessage ="El campo {0} es requerido.")]
        public string? Titulo { get; set; }

        [Column("publico")]
        public bool? Publico { get; set; }

        
    }
}
