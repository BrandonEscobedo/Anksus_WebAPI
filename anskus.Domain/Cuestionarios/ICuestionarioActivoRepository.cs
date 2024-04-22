using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public interface ICuestionarioActivoRepository
    {
        Task<bool> IsCodeValid(int code);
        Task<int> ActivarCuestionario(int idcuestionario, string email);

    }
}
