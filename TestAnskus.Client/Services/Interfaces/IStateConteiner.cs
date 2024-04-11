using Anksus_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Components;
using TestAnskus.Client.Services.Implementacion;
using TestAnskus.Shared;
namespace TestAnskus.Client.Services.Interfaces
{
    public interface IStateConteiner
    {
        public event Action<ParticipanteEnCuestDTO>? OnAgregarUsuario;
        event Action? Omitir;
        event Action<int>? OnSiguientePregunta;
        public event Action OnIniciarCuestionario;

        List<ParticipanteEnCuestDTO> participanteEnCuest { get; }
        ParticipanteEnCuestDTO Participante { get; set; }
        List<CuestionarioDTO> Cuestionario { get;}
        public int IdPregunta { get; set; }
        public int codigo { get; set; } 
        List<PreguntasDTO> preguntas { get;  } 
        public void RemoveParticipante(ParticipanteEnCuestDTO participante);
        public void AddParticipante(ParticipanteEnCuestDTO participante);
        void SetParticipante(ParticipanteEnCuestDTO participante);
        public List<CuestionarioDTO> GetCuestionario();
        public Task SetCuestionario(List<CuestionarioDTO> cuestionario, int codigo);
        public Task SiguientePregunta(int idpregunta);
            void ChangeState(int pregunta);
        public Task<DatosPregunta> IndiceSiguientePregunta();
        public  Task IniciarCuestionario(List<PreguntasDTO> cuestionario);


    }
}
