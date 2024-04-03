using Anksus_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Components;
using TestAnskus.Client.Services.Interfaces;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion
{
    public class StateConteiner : IStateConteiner
    {
        public ParticipanteEnCuestDTO Participante { get; set; } = new();
        public event Action? StateChange;
        public event Action? Omitir;
        public event Action<int>? StatePregunta;
        List<ParticipanteEnCuestDTO> participanteEnCuest=new(); 
        public List<CuestionarioDTO> cuestionario { get; private set; } = new();
        public int CantidadPreguntas=0;
        private readonly NavigationManager _navigationManager;
        public StateConteiner(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
        public void SiguientePregunta(int NumeroPregunta)
        {
            ChangeState(NumeroPregunta);
        }
        public void SetCuestionario(List<CuestionarioDTO> cuestionario)
        {
            this.cuestionario = cuestionario;
            CantidadPreguntas = cuestionario.Count();
        }
        public List<CuestionarioDTO> GetCuestionario()
        {
            return cuestionario;
        }

        private void ChangeState(int pregunta) => StatePregunta?.Invoke(pregunta);
        public void SetParticipante(ParticipanteEnCuestDTO participante)
        {
            Participante = participante;
            participanteEnCuest.Add(participante);
            StateChange?.Invoke();
        }
        void IStateConteiner.ChangeState(int pregunta)
        {
            throw new NotImplementedException();
        }
    }    
}
