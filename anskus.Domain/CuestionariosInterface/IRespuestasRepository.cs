using Anksus_WebAPI.Models.dbModels;

namespace anskus.Domain.CuestionariosInterface
{
    public interface IRespuestasRepository
    {
        void Add(List<Respuesta> respuestas);
        void Delete(int id);
    }
}