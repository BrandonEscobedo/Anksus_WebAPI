using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Interfaces.CuestionarioActivo
{
    public interface ICuestionarioAService
    {
        public  Task<ResponseAPI<CuestionarioActivoDTO>> ActivarCuestionario(int id);
        public  Task<ResponseAPI<CuestionarioActivoDTO>> FinalizarCuestionario(int id);

    }
}
