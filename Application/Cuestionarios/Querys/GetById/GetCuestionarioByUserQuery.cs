using anskus.Application.DTOs.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Querys.GetById
{
    public  record GetCuestionarioByUserQuery(string email):IRequest<List<CuestionarioDTO>>;


}
