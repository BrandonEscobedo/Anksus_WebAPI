using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using TestAnskus.Client.Services.Interfaces.Hub;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion.Hub
{
    public class HubConnecionService:IHubConnectionService
    {
        private readonly HubConnection _hubConnection;
        public HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public HubConnecionService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _hubConnection = new HubConnectionBuilder()
                  .WithUrl("https://localhost:7150/ChatCuest")
                  .Build();
            _hubConnection.On<string>("Usuario Conectado", (usuario) =>
            {

            });
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }
        public async Task IngresarSala(string codigo, string nombre)
        {
            _navigationManager.NavigateTo($"CuestionarioActivo");
        }
        public async Task IniciarConexion()
        {
            await _hubConnection.StartAsync();
        }
        public async Task<bool> VerificarCodigo(int code)
        {
            var result = await _httpClient.GetFromJsonAsync<bool>($"api/ParticipantesEnCuestionario/{code}");
            return result;
        }
    }
}
