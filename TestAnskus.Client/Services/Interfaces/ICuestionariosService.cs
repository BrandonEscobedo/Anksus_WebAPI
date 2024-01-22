using Anksus_WebAPI.Models.DTO;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Interfaces
{
    public interface ICuestionariosService
    {
        Task<ResponseAPI<List<CuestionarioDTO>>> GetCuestionario();
        Task<int> CreateCuestionario(CuestionarioDTO cuestionario);
        Task<int> EditCuestionario(CuestionarioDTO cuestionario);
        Task<bool> DeleteCuestionario(int id );


    }
}
