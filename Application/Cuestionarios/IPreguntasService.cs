using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;

namespace anskus.Application.Cuestionarios
{
    public interface IPreguntasService
    {
         Task<PreguntasResponse> Add(PreguntasDTO pregunta);
         Task<PreguntasResponse> GetPreguntas(int pregunta);
         Task<PreguntasResponse> DeletePregunta(int pregunta);

    }

}
