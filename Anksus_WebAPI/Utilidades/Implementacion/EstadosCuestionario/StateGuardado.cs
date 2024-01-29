using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;
using Anksus_WebAPI.Server.Utilidades.Interfaces.EstadosCuestionario;
using Microsoft.AspNetCore.Components;

namespace Anksus_WebAPI.Server.Utilidades.Implementacion.EstadosCuestionario
{
    
    public class StateGuardado : IStateCuestionario
    {
        public void Activo(Cuestionario cuestionario)
        {
            cuestionario.Estado = false;
        }

        public void Finalizado(Cuestionario cuestionario)
        {
            cuestionario.Estado = false;
        }

        public void Guardado(Cuestionario cuestionario)
        {
            cuestionario.Estado = false;
        }

        public void SetEstado(Cuestionario cuestionario)
        {
            throw new NotImplementedException();
        }
    }
}
