using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Preguntas.Delete
{
  public  record class DeletePreguntaCommand(int id):IRequest<bool>;
}
