using anskus.Application.DTOs.Cuestionarios;

namespace anskus.Application.HubSignalr.StateContainerHub
{
    public interface IHubStateCreador
    {
        List<ParticipanteEnCuestDTO> ListaPuntos { get; set; }

        public event Action? OnListaPuntos;

        void AddParticipantePuntos(ParticipanteEnCuestDTO participante);
    }
}