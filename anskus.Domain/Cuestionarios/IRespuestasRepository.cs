using anskus.Domain.Models.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public interface IRespuestasRepository
    {
        Task<bool> Add(List<Respuesta> preguntas);

    }
}
