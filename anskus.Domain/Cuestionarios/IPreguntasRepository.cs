using anskus.Domain.Models.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public interface IPreguntasRepository
    {
          Task<int> Add(Pregunta pregunta);
          Task<bool> Delete(int id);
    }
}
