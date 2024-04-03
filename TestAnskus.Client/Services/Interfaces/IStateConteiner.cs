using Anksus_WebAPI.Models.DTO;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Interfaces
{
    public interface IStateConteiner
    {
        event Action  StateChange;
        event Action? Omitir;
        event  Action<int>? StatePregunta;
      
        ParticipanteEnCuestDTO Participante { get; set; } 
        void SetParticipante(ParticipanteEnCuestDTO participante);
        public List<CuestionarioDTO> GetCuestionario();
        void SetCuestionario(List<CuestionarioDTO> cuestionario);
        void SiguientePregunta(int NumeroPregunta);
        void ChangeState(int pregunta);


       
    }
}
