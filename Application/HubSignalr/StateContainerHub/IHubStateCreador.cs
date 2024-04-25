using anskus.Application.DTOs.Cuestionarios;

namespace anskus.Application.HubSignalr.StateContainerHub
{
    public interface IHubStateCreador
    {
        List<ParticipanteEnCuestDTO> ListaPuntos { get; set; }

        event Action<List<ParticipanteEnCuestDTO>>? OnListaPuntos;

        void AddParticipantePuntos(ParticipanteEnCuestDTO participante);
    }
}