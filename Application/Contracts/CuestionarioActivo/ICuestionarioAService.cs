using anskus.Application.DTOs.Cuestionarios;

namespace TestAnskus.Client.Services.Interfaces.CuestionarioActivo
{
    public interface ICuestionarioAService
    {
        public  Task<ResponseAPI<CuestionarioActivoDTO>> ActivarCuestionario(int id);
        public  Task<ResponseAPI<CuestionarioActivoDTO>> FinalizarCuestionario(int id);
        public  Task<bool> VerificarCodigo(int code);
    }
}
