using Anksus_WebAPI.Models.DTO;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Interfaces
{
    public interface IStateConteiner
    {
        public event Action<ParticipanteEnCuestDTO>? StateChange;
        event Action? Omitir;
        event Action<int>? StatePregunta;
        List<ParticipanteEnCuestDTO> participanteEnCuest { get; }
        ParticipanteEnCuestDTO Participante { get; set; }
        List<CuestionarioDTO> Cuestionario { get; }
        public List<PreguntasDTO> preguntas { get;  } 
        public List<RespuestasDTO> respuestas { get;  } 
        public void RemoveParticipante(ParticipanteEnCuestDTO participante);
        public void AddParticipante(ParticipanteEnCuestDTO participante);
        void SetParticipante(ParticipanteEnCuestDTO participante);
        public List<CuestionarioDTO> GetCuestionario();
        void SetCuestionario(List<CuestionarioDTO> cuestionario);
        void SiguientePregunta(int NumeroPregunta);
        void ChangeState(int pregunta);



    }
}
