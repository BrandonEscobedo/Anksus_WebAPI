using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.Querys.VerificarCodigoCuestionario
{
    public record VerificarCodigoQuery(int codigo):IRequest<bool>;
}
