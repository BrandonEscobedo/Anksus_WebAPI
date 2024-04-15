using anskus.Application.DTOs.Cuestionarios;

namespace TestAnskus.Client.Services.Interfaces
{
    public interface IRespuestasService
    {
        Task<int> CreateRespuestas(List<RespuestasDTO> respuestasDTO);

    }
}
