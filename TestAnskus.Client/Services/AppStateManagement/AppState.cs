using Anksus_WebAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.AppStateManagement
{
    public class AppState
    {
        public List<CuestionarioDTO> cuestionario { get; private set; } = new();
        public Action? Omitir;
        public Action<int>? StatePregunta ;
        public int CantidadPreguntas;
        public void SiguientePregunta(int NumeroPregunta)
        {          
                ChangeState(NumeroPregunta);         
        }
        public void SetCuestionario(List<CuestionarioDTO> cuestionario)
        {
            this.cuestionario = cuestionario;
            CantidadPreguntas = cuestionario.Count();
        }
        private void ChangeState(int pregunta) => StatePregunta?.Invoke(pregunta);
    }
}
