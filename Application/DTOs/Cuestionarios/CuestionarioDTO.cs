using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Anksus_WebAPI.Models.dbModels;

namespace anskus.Application.DTOs.Cuestionarios
{
    public class CuestionarioDTO
    {
       public int IdCuestionario { get; set; }

        public int IdUsuario { get; set; }

        public int IdCategoria { get; set; }

        public string? Titulo { get; set; }

        public bool Publico { get; set; }

        public string? Email { get; set; }

    }
}
