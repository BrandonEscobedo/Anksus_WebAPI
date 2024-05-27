using anskus.Application.DTOs.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Respuestas.Create
{
    public record CreateRespuestasCommand(List<RespuestasDTO> respuestas):IRequest<bool>;
 
}
