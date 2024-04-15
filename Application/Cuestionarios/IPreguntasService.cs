using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios
{
    public interface IPreguntasService
    {
         Task<GeneralResponse> Add(PreguntasDTO pregunta);

    }

}
