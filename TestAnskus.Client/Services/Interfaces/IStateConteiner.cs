using Anksus_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Components;
using TestAnskus.Client.Services.Implementacion;
using TestAnskus.Shared;
namespace TestAnskus.Client.Services.Interfaces
{
    public interface IStateConteiner
    {
        public event Action<ParticipanteEnCuestDTO>? OnAgregarUsuario;
        public event Action<PreguntasDTO, string?>? OnSiguientePregunta;

        List<ParticipanteEnCuestDTO> participanteEnCuest { get; }
        ParticipanteEnCuestDTO Participante { get; set; }
        CuestionarioDTO Cuestionario { get;}
        public string titulo { get; set; }
     
        PreguntasDTO pregunta { get; set; } 
        public void RemoveParticipante(ParticipanteEnCuestDTO participante);
        public void AddParticipante(ParticipanteEnCuestDTO participante);
        void SetParticipante(ParticipanteEnCuestDTO participante);
        public CuestionarioDTO GetCuestionario();
        public Task SetCuestionario(CuestionarioDTO cuestionario, int codigo);
        public void SiguientePregunta(PreguntasDTO pregunta,string? titulo);
       
      


    }
}
