using Anksus_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Components;
using TestAnskus.Client.Pages.EnJuego.Compartido;
using TestAnskus.Client.Services.Interfaces;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion
{
     
   
    public class StateConteiner : IStateConteiner
    {
        
        public ParticipanteEnCuestDTO Participante { get; set; } = new();
        public event Action<ParticipanteEnCuestDTO>? StateChange;
        public event Action? Omitir;
        public event Action<int>? StatePregunta;
        public List<ParticipanteEnCuestDTO> participanteEnCuest { get; set; } = new();
        public List<CuestionarioDTO> Cuestionario { get; set; } = new();
        public List<PreguntasDTO> preguntas { get; set; }= new();
        public List<RespuestasDTO> respuestas { get; set; }= new();
        private readonly NavigationManager _navigationManager;

        public StateConteiner(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public int CantidadPreguntas=0;
            public void AddParticipante(ParticipanteEnCuestDTO participante)
            {
            participanteEnCuest.Add(participante);
            StateChange?.Invoke(participante);
        }
        public void RemoveParticipante(ParticipanteEnCuestDTO participante)
        {
            var user = participanteEnCuest
                .Where(x => x.IdPeC ==participante.IdPeC)
                .FirstOrDefault();
            if (user != null)
            {
                participanteEnCuest.Remove(user);
                StateChange?.Invoke(participante);
            }   
        }
        public void SepararObjetos(List<CuestionarioDTO> cuestionarios)
        {
            foreach(var preguntasCuest in cuestionarios)
            {
                preguntas = preguntasCuest.Pregunta.ToList();
                foreach (var preg in preguntasCuest.Pregunta)
                {
                    respuestas=preg.Respuesta.ToList();
                }
            }
        }
        public void SiguientePregunta(int NumeroPregunta)
        {
            ChangeState(NumeroPregunta);
        }
        public void SetCuestionario(List<CuestionarioDTO> cuestionario)
        {
            Cuestionario = cuestionario;
            SepararObjetos(cuestionario);
            CantidadPreguntas = cuestionario.Count();
        }
        public List<CuestionarioDTO> GetCuestionario()
        {
            return Cuestionario;
        }

        private void IniciarCuestionario()
        {
            _navigationManager.NavigateTo("");
        }

        private void ChangeState(int pregunta) => StatePregunta?.Invoke(pregunta);
        public void SetParticipante(ParticipanteEnCuestDTO participante)
        {
            Participante = participante;
        }
        void IStateConteiner.ChangeState(int pregunta)
        {
            throw new NotImplementedException();
        }
    }    
}
