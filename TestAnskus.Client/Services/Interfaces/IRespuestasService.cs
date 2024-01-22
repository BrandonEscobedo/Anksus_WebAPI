using Anksus_WebAPI.Models.DTO;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Interfaces
{
    public interface IRespuestasService
    {
        Task<int> CreateRespuestas(List<RespuestasDTO> respuestasDTO);

    }
}
