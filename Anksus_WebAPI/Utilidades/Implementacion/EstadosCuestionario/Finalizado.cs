using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Server.Utilidades.Interfaces.EstadosCuestionario;

namespace Anksus_WebAPI.Server.Utilidades.Implementacion.EstadosCuestionario
{
    public class Finalizado : IStateCuestionario
    {
        public void Activo(Cuestionario cuestionario)
        {
            cuestionario.Estado = false;
        }

        public void Guardado(Cuestionario cuestionario)
        {
            cuestionario.Estado = false;
        }

        public void SetEstado(Cuestionario cuestionario)
        {
            cuestionario.Estado = false;
        }

        void IStateCuestionario.Finalizado(Cuestionario cuestionario)
        {
            throw new NotImplementedException();
        }
    }
}
