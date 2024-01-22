using Anksus_WebAPI.Models.DTO;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Interfaces
{
    public interface IPreguntasService
    {
        Task<PreguntasDTO> GetPreguntas(int id);
        Task<int> CreatePregunta(PreguntasDTO preguntasDTO);
        Task<bool> EditarPregunta(PreguntasDTO preguntaDTO);
        Task<bool> DeletePregunta(int id);
    }
}
