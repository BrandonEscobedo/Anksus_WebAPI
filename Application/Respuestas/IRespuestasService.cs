using anskus.Application.DTOs.Cuestionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Respuestas
{
    public interface IRespuestasServices
    {
        Task<bool> Add(List<RespuestasDTO> respuestas);
    }


}
