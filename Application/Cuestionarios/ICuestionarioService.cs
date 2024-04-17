using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios
{
    public interface ICuestionarioService
    {
        public  Task<int> Add(CuestionarioDTO cuestionario);
        public  Task<CuestionarioResponse> Update(CuestionarioDTO cuestionario);
    }
}
