using Anksus_WebAPI.Models.dbModels;
using anskus.Application.DTOs.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Create
{
  public record CreateCuestionarioCommand(CuestionarioDTO Cuestionario):IRequest<int>; 
   
}
