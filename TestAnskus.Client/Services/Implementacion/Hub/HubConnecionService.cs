using Anksus_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TestAnskus.Client.Services.Interfaces;
using TestAnskus.Client.Services.Interfaces.Hub;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion.Hub
{
    public class HubConnecionService : IAsyncDisposable
    {
        HubConnection _hubConnection;
        public event Action<ParticipanteEnCuestDTO> NewParticipante;
        public event Action<ParticipanteEnCuestDTO> removeParticipante;
        public HttpClient _httpClient;
        public List<ParticipanteEnCuestDTO> partipantesEnCuestionario=new();
        private readonly IStateConteiner _stateConteiner;
        private readonly NavigationManager navigationManager;
        public HubConnecionService(HttpClient httpClient,
            HubConnection hubConnection,
            NavigationManager navigationManager,
            IStateConteiner stateConteiner
             )
        {
            _httpClient = httpClient;
            _hubConnection = hubConnection;
            _hubConnection.On<ParticipanteEnCuestDTO>("NewParticipante", part =>
            {
                NewParticipante?.Invoke(part);
            }
            );
            _hubConnection.On<ParticipanteEnCuestDTO>("RemoveUser", part =>
            {
                removeParticipante?.Invoke(part);
            });
            this.navigationManager = navigationManager;
            _stateConteiner = stateConteiner;
        }


        public async Task NewRom(int codigo,int idcuestionario) 
        {
            bool result = await _hubConnection.InvokeAsync<bool>("CreateRoom", codigo.ToString());
            if(result)
            {
                var Cuestionario = await _httpClient.GetFromJsonAsync<List<CuestionarioDTO>>($"api/Cuestionarios/{idcuestionario}") ?? throw new Exception("Error al encontrar el cuestionario");
                if(Cuestionario != null)
                {
                    _stateConteiner.SetCuestionario(Cuestionario);
                }
                navigationManager.NavigateTo($"/Lobby/{codigo}");                         
            }
        }
        public async Task AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            bool result = await _hubConnection.InvokeAsync<bool>("AddUserToRoom", participante);
            if (result == true)
            {
                _stateConteiner.SetParticipante(participante);
                partipantesEnCuestionario.Add(participante);
                navigationManager.NavigateTo("/Sala");
            }
        }
        public async Task GetUsers(int code)
        {
           await _hubConnection.SendAsync("GetUsersByRoom", code);
          
        }
        public async Task UserLeft(ParticipanteEnCuestDTO participante)
        {
            await _hubConnection.SendAsync("UserLeftRoomUserLeftRoom", participante);
            partipantesEnCuestionario.Remove(participante);
        }
        public void RemoveRoom(int codigo)
        {
             _hubConnection.SendAsync("RemoveRoom", codigo);
        }
        public async Task<bool> VerificarCodigo(int code)
        {   
            var result = await _httpClient.
            GetFromJsonAsync<bool>($"api/ParticipantesEnCuestionario/{code}");
            return result;
        }
        public async ValueTask DisposeAsync()
        {
            //if (_hubConnection.ConnectionId != null)
            //{
            //    try
            //    {
            //        await _hubConnection.StopAsync();
            //    }
            //    finally
            //    {
            //        await _hubConnection.DisposeAsync();
            //        _hubConnection = null!;

            //    }

            //}
        }
    }
}
