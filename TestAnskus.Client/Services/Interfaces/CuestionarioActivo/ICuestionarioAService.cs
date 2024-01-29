using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Interfaces.CuestionarioActivo
{
    public interface ICuestionarioAService
    {
        public  Task<ResponseAPI<int>> ActivarCuestionario(int id);
        public  Task<ResponseAPI<int>> FinalizarCuestionario(int id);

    }
}
