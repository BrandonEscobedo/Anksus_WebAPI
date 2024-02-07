﻿using Microsoft.AspNetCore.Components;
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
        public event Action<int> OnUpdateCount;
        public HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public HubConnecionService(HttpClient httpClient, NavigationManager navigationManager)
        {     
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }
        public async Task IngresarSala(string codigo, string nombre)
        {
            _navigationManager.NavigateTo($"CuestionarioActivo");
        }
        public async Task SendMessage(ParticipanteEnCuestDTO participante)
        {
            if (_httpClient is not null) return;
            await _hubConnection.SendAsync("Sendusuario",participante);

        }
        public async Task IniciarConexion()
        {
            if (_hubConnection is not null) return;
            _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7150/ChatCuest")
            .Build();
            _hubConnection.On<ParticipanteEnCuestDTO>("receiveParticipante", usuario => participante?.Invoke(usuario));
            _hubConnection.On<int>("UpdatesClientsCount",count=>OnUpdateCount(count));
            await _hubConnection.StartAsync();
        }
        public async Task<bool> VerificarCodigo(int code)
        {
            var result = await _httpClient.GetFromJsonAsync<bool>($"api/ParticipantesEnCuestionario/{code}");
            return result;
        }

        public async ValueTask DisposeAsync()
        {
            if(_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
            }
        }
    }
}
