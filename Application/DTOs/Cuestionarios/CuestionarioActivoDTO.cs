using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.DTOs.Cuestionarios
{
    public class CuestionarioActivoDTO
    {
        public int idcuestionario { get; set; }
        public int codigo { get; set; }
        public CuestionarioDTO Cuestionario { get; set; }=new();
    }
}
