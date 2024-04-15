using Anksus_WebAPI.Models.dbModels;

namespace anskus.Domain.CuestionariosInterface
{
    public interface IPreguntasRepository
    {
        void Add(Pregunta pregunta);
        void Delete(int id);
        Task<Pregunta> GetByIdAsync(int id);
        void Update(int id);
    }
}