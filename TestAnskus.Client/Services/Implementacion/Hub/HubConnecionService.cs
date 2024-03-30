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
        private readonly List<ParticipanteEnCuestDTO> participantesActivos = new();
        public IReadOnlyList<ParticipanteEnCuestDTO> ParticipantesActivos => participantesActivos;
        public event Action<List<ParticipanteEnCuestDTO>> ParticipanteList;
        private readonly IStateConteiner _stateConteiner;
        private readonly NavigationManager navigationManager;
        private readonly IJSRuntime _jSRuntime;
        public HubConnecionService(HttpClient httpClient,
            HubConnection hubConnection,
            NavigationManager navigationManager,
            IStateConteiner stateConteiner,
            IJSRuntime jSRuntime)
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
            _jSRuntime = jSRuntime;
        }


        public async Task NewRom(int codigo,int idcuestionario) 
        {
            bool result = await _hubConnection.InvokeAsync<bool>("CreateRoom", codigo.ToString());
            if(result)
            {
                List<CuestionarioDTO> response = await _httpClient.GetFromJsonAsync<List<CuestionarioDTO>>($"api/Cuestionarios/{idcuestionario}") ?? throw new Exception("Error al encontrar el cuestionario");
                if (response != null)
                {
                    var responseJson = System.Text.Json.JsonSerializer.Serialize<List<CuestionarioDTO>>(response);
                    await _jSRuntime.InvokeVoidAsync("sessionStorage.setItem", "Llave", responseJson);
                    navigationManager.NavigateTo($"/Lobby/{codigo}");
                }             
            }
        }
        public async Task AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            bool result = await _hubConnection.InvokeAsync<bool>("AddUserToRoom", participante);
            if (result == true)
            {
                _stateConteiner.SetValor(participante);
                participantesActivos.Add(participante);
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
            participantesActivos.Remove(participante);
        }
        public async Task RemoveRoom(int codigo)
        {
            await _hubConnection.SendAsync("RemoveRoom", codigo);
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
