using anskus.Application.DTOs.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.Commands.Create
{
    public record CreateCuestionarioActivoCommand(int idcuestionario, string email, int codigo = 0) : IRequest<CuestionarioActivoDTO>;
}
