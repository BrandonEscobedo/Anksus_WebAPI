using anskus.Application.DTOs.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Commands.Update
{
    public record UpdateCuestionarioCommand(CuestionarioDTO Cuestionario) : IRequest<bool>;
}
