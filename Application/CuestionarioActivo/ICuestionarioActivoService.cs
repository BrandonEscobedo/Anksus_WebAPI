using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo
{
    public interface ICuestionarioActivoService
    {
        Task<int> ActivarCuestionario(int idcuestionario);
        Task<bool> VerificarCodigoEntrar(int code);
     }
}
