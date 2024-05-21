using anskus.Application.DTOs.Cuestionarios;
        
namespace anskus.Application.CuestionarioActivo
{
    public interface ICuestionarioActivoService
    {
        Task<CuestionarioActivoDTO> ActivarCuestionario(int idcuestionario);
        Task<bool> VerificarCodigoEntrar(int code);
     }
}
