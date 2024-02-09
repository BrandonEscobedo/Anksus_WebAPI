using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using TestAnskus.Client.Services.Interfaces.Hub;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion.Hub
{
    public class HubConnecionService:IAsyncDisposable
    {
        HubConnection _hubConnection;
        public event Action<ParticipanteEnCuestDTO> participante;
        public event Action<List<string>> usuariosSala;
        public event Action<int> OnUpdateCount;
        public HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public HubConnecionService(HttpClient httpClient, NavigationManager navigationManager)
        {     
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task SendMessage(string  participante,int codigo)
        {
            await _hubConnection.SendAsync("IngresarUsuario", participante,codigo);
            
        }
        public async Task IniciarConexion()
        {
            if (_hubConnection is null)
            {
            _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7150/ChatCuest")
            .Build();
            _hubConnection.On<List<string>>("UsuariosEnLaSala", usuarios => usuariosSala?.Invoke(usuarios));
            _hubConnection.On<ParticipanteEnCuestDTO>("receiveParticipante", usuario => participante?.Invoke(usuario));
            _hubConnection.On<int>("UpdatesClientsCount",count=>OnUpdateCount(count));
            await _hubConnection.StartAsync();
                await _hubConnection.InvokeAsync()
            }

        }
        public async Task<bool> VerificarCodigo(int code)
        {
            var result = await _httpClient.GetFromJsonAsync<bool>($"api/ParticipantesEnCuestionario/{code}");
            return result;
        }

        public async ValueTask DisposeAsync()
        {
            if(_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
            }
        }
    }
}
