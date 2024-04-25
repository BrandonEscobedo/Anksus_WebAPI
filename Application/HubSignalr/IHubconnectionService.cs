using anskus.Application.DTOs.Cuestionarios;

namespace anskus.Application.HubSignalr
{
    public interface IHubconnectionService
    {
        Task AddUserToRoom(ParticipanteEnCuestDTO participante);
        Task NewRom(CuestionarioActivoDTO cuestionarioActivo);
        Task SiguientePreguntaUsuarios();
        Task ContestarPregunta(ParticipanteEnCuestDTO participante );
    }
}