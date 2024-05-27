using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios
{
    public interface IIdContainer
    {

        public int idCuestionario { get; set; }
        public int idPregunta { get; set; }
        public void SetIdCuestionario(int id);

        public void SetIdPregunta(int id);
    }
}
