using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.Extensions;
using anskus.Application.HubSignalr.StateContainerHub;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace anskus.Application.HubSignalr
{
    public class HubconnectionService : IHubconnectionService
    {
        HubConnection _hubConnection;
        private readonly IStateConteiner _stateConteiner;
        private readonly IHubStateCreador _hubStateCreador;
        private readonly NavigationManager navigationManager;
        public HubconnectionService( 
            HubConnection hubConnection,
            NavigationManager navigationManager,
            IStateConteiner stateConteiner,
            IHubStateCreador hubStateCreador
             )
        {
          
            _hubConnection = hubConnection;

            _hubConnection.On<ParticipanteEnCuestDTO>("NewParticipante", OnNewParticipante);
            _hubConnection.On<PreguntasDTO, string?>("SiguientePregunta", OnSiguientePregunta);
            _hubConnection.On<ParticipanteEnCuestDTO>("RemoveUser", RemoveParticipante);
            _hubConnection.On<List<ParticipanteEnCuestDTO>>("ListaRanking", OnListaRanking);
            _hubConnection.On<ParticipanteEnCuestDTO>("PreguntaContestada", OnPreguntaContestada);
            this.navigationManager = navigationManager;
            _hubStateCreador = hubStateCreador;
            _stateConteiner = stateConteiner;
        }

        private void OnListaRanking(List<ParticipanteEnCuestDTO> list)
        {
            _stateConteiner.SetListaRanking(list);
        }

        private void OnPreguntaContestada(ParticipanteEnCuestDTO participante)
        {
            _hubStateCreador.AddParticipantePuntos(participante);
        }
        private void OnSiguientePregunta(PreguntasDTO idpregunta, string? titulo)
        {
            _stateConteiner.SiguientePregunta(idpregunta, titulo);
        }
        public async Task SiguientePreguntaUsuarios()
        {
            await _hubConnection.InvokeAsync("SiguientePregunta");
        }
        private void OnNewParticipante(ParticipanteEnCuestDTO participante) => _stateConteiner.AddParticipante(participante);
        private void RemoveParticipante(ParticipanteEnCuestDTO participante) => _stateConteiner.RemoveParticipante(participante);
        public async Task NewRom(CuestionarioActivoDTO cuestionarioActivo)
        {       
            if (cuestionarioActivo != null)
            {
                List<PreguntasDTO> preguntas = [.. cuestionarioActivo.Cuestionario.Pregunta.ToList()];

                bool result = await _hubConnection.
                    InvokeAsync<bool>("CreateRoom", cuestionarioActivo.codigo, preguntas, cuestionarioActivo.Cuestionario.Titulo);
                _stateConteiner.titulo = cuestionarioActivo.Cuestionario!.Titulo!;
                await _stateConteiner.SetCuestionario(cuestionarioActivo.Cuestionario, cuestionarioActivo.codigo);      
                if (result)
                {
                    navigationManager.NavigateTo($"/Lobby/{cuestionarioActivo.codigo}");
                }
            }
        }
        public async Task EnviarRanking()
        {
            
        await _hubConnection.InvokeAsync("EnviarRankingUsuarios", _hubStateCreador.ListaPuntos);
            
        }
        public async Task AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            bool result = await _hubConnection.
                InvokeAsync<bool>("AddUserToRoom", participante);
            if (result == true)
            {
                _stateConteiner.SetParticipante(participante);
                navigationManager.NavigateTo("/Sala");
            }
        }

        public async Task ContestarPregunta(ParticipanteEnCuestDTO participante )
        {
            await _hubConnection.InvokeAsync("ContestarPregunta", participante);
        }
    }
}
