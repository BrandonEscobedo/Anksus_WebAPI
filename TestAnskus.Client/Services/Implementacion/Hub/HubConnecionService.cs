using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using TestAnskus.Client.Services.Interfaces.Hub;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion.Hub
{
    public class HubConnecionService : IAsyncDisposable
    {
        HubConnection _hubConnection;
        public event Action<ParticipanteEnCuestDTO> participante;
        public event Action<List<ParticipanteEnCuestDTO>> usuariosSala;
        public event Action<int> OnUpdateCount;
        public HashSet<Action<ParticipanteEnCuestDTO>> name = new();
        public HttpClient _httpClient;
        private readonly NavigationManager navigationManager;
        public HubConnecionService(HttpClient httpClient, HubConnection hubConnection, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _hubConnection = hubConnection;
            _hubConnection.On<ParticipanteEnCuestDTO>("NewParticipante", part =>
            {
              
                participante?.Invoke(part);
                Console.Write("Usuario Agregado", part);

            }
            );
            _hubConnection.On<int>("UsuariosEnLaSala", count =>
            {
                OnUpdateCount?.Invoke(count);
                Console.WriteLine(count);
            });
            this.navigationManager = navigationManager;
        }
        public async Task CountUpdate(int onUpdate) => OnUpdateCount?.Invoke(onUpdate);

        public async Task NewRom(string codigo) => await _hubConnection.InvokeAsync("CreateRoom", codigo);
        public async Task AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            bool result = await _hubConnection.InvokeAsync<bool>("AddUserToRoom", participante);
            if (result == true)
            {
                await CountUpdate(1);
                navigationManager.NavigateTo("/Sala");
            }
        }
        public async Task GetUsers(int code)
        {
           await _hubConnection.SendAsync("GetUsersByRoom", code);
          
        }
        public async Task<bool> VerificarCodigo(int code)
        {
            var result = await _httpClient.
                GetFromJsonAsync<bool>($"api/ParticipantesEnCuestionario/{code}");
            return result;
        }

        public async ValueTask DisposeAsync( )
        {
            if(_hubConnection.ConnectionId !=null)
            {
                try
                {
                    await _hubConnection.StopAsync();
                }
                finally
                {
                    await _hubConnection.DisposeAsync();
                    _hubConnection = null!;

                }

            }
        }
    }
}
