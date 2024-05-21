using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;

namespace anskus.Application.Preguntas
{
    public interface IPreguntasService
    {
        Task<int> Add(PreguntasDTO pregunta);
        Task<PreguntasResponse> GetPreguntas(int pregunta);
        Task<bool> DeletePregunta(int pregunta);
        public  Task<bool> UpdatePregunta(PreguntasDTO preguntas);

    }

}
