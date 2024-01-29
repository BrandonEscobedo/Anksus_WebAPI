using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;

namespace Anksus_WebAPI.Server.Utilidades.Interfaces.EstadosCuestionario
{
    public interface IStateCuestionario
    {
        void Guardado(Cuestionario cuestionario);
        void Activo(Cuestionario cuestionario);
        void Finalizado(Cuestionario cuestionario);
        void SetEstado(Cuestionario cuestionario);
    }
}
