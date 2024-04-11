using Anksus_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Components;
using TestAnskus.Client.Pages.ComponentsBase;
using TestAnskus.Client.Pages.EnJuego.Compartido;
using TestAnskus.Client.Services.Implementacion.Hub;
using TestAnskus.Client.Services.Interfaces;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion
{ 

    public class StateConteiner : IStateConteiner
    {
        public ParticipanteEnCuestDTO Participante { get; set; } = new();
        public event Action<ParticipanteEnCuestDTO>? OnAgregarUsuario;
        public event Action? Omitir;
        public event Action<int>? OnSiguientePregunta;
        public event Action OnIniciarCuestionario;
        public int IdPregunta { get; set; } = 0;
        public int codigo { get; set; } = 0;
        public List<ParticipanteEnCuestDTO> participanteEnCuest { get; set; } = new();
        public List<CuestionarioDTO> Cuestionario { get; set; } = new();
        public List<PreguntasDTO> preguntas { get; set; } = new();

        private readonly NavigationManager _navigationManager;

        public StateConteiner(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
        
            public int CantidadPreguntas=0;
            public void AddParticipante(ParticipanteEnCuestDTO participante)
        {    
            participanteEnCuest.Add(participante);
            OnAgregarUsuario?.Invoke(participante);
        }
        public void RemoveParticipante(ParticipanteEnCuestDTO participante)
        {
            var user = participanteEnCuest
                .Where(x => x.IdPeC ==participante.IdPeC)
                .FirstOrDefault();
            if (user != null)
            {
                participanteEnCuest.Remove(user);
                OnAgregarUsuario?.Invoke(participante);
            }   
            
        }
        public void ChangeState(int pregunta)
        {
            OnSiguientePregunta?.Invoke(pregunta);
        }

        public async Task CrearPreguntas(List<PreguntasDTO> Preguntas)
        {
            preguntas=Preguntas;
        }
        public async Task IniciarCuestionario(List<PreguntasDTO> Preguntas)
        {
                
                preguntas = Preguntas;
                OnSiguientePregunta?.Invoke(preguntas.First().IdPregunta);
               
        }

        public async Task SetCuestionario(List<CuestionarioDTO> cuestionario, int codigo)
        {
            Cuestionario = cuestionario;
            this.codigo = codigo;
        }

        public List<CuestionarioDTO> GetCuestionario()
        {
            return Cuestionario;
        }
        public async Task<DatosPregunta> IndiceSiguientePregunta()
        {
            DatosPregunta datos = new DatosPregunta { Idpregunta = preguntas.First().IdPregunta, CodigoRoom = codigo };
          
            return datos;
        }
        public async Task SiguientePregunta(int idpregunta)
        {
            this.IdPregunta = idpregunta;
            OnSiguientePregunta?.Invoke(idpregunta);
          
        }
        public void SetParticipante(ParticipanteEnCuestDTO participante)
        {
            Participante = participante;
        }

    }    
    public class DatosPregunta
    {
        public int Idpregunta { get; set; }
        public int CodigoRoom { get; set; }
    }
}
