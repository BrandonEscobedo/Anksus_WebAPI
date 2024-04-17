using anskus.Application.DTOs.Cuestionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.DTOs.Response.Cuestionarios
{
    public record CuestionarioResponse(int idcuestionario, string? Message = null!, bool Flag = false);  

}
