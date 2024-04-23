using anskus.Application.DTOs.Cuestionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo
{
    public interface ICuestionarioActivoService
    {
        Task<CuestionarioActivoDTO> ActivarCuestionario(int idcuestionario);
        Task<bool> VerificarCodigoEntrar(int code);
     }
}
