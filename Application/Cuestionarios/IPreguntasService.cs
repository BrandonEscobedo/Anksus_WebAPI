using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;

namespace anskus.Application.Cuestionarios
{
    public interface IPreguntasService
    {
         Task<GeneralResponse> Add(PreguntasDTO pregunta);

    }

}
