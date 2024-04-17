using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using TestAnskus.Client.Services.Interfaces;
using anskus.Application.DTOs.Cuestionarios;

namespace TestAnskus.Client.Services.Implementacion.Hub
{
    public sealed class HubConnecionService
    {
        HubConnection _hubConnection;
        public HttpClient _httpClient;
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

            _hubConnection.On<ParticipanteEnCuestDTO>("NewParticipante", OnNewParticipante);
            _hubConnection.On<PreguntasDTO, string?>("SiguientePregunta",OnSiguientePregunta );
            _hubConnection.On<ParticipanteEnCuestDTO>("RemoveUser", RemoveParticipante);
            this.navigationManager = navigationManager;
            _stateConteiner = stateConteiner;

        }
        private void OnSiguientePregunta(PreguntasDTO idpregunta, string? titulo )
        {
            _stateConteiner.SiguientePregunta(idpregunta, titulo);
        }
        public async Task SiguientePreguntaUsuarios()
        {             
            await _hubConnection.InvokeAsync("SiguientePregunta");
        }
        private void OnNewParticipante(ParticipanteEnCuestDTO participante) => _stateConteiner.AddParticipante(participante);
        private void RemoveParticipante(ParticipanteEnCuestDTO participante) => _stateConteiner.RemoveParticipante(participante);
        public async Task NewRom(int codigo, int idcuestionario)
        {
            var Cuestionario = await _httpClient.
                   GetFromJsonAsync<CuestionarioDTO>($"api/Cuestionarios/{idcuestionario}")
                   ?? throw new Exception("Error al encontrar el cuestionario");
            if (Cuestionario != null)
            {
                //List<PreguntasDTO> preguntas = [.. Cuestionario];

                //bool result = await _hubConnection.
                //    InvokeAsync<bool>("CreateRoom", codigo, preguntas,Cuestionario.Titulo);
                //_stateConteiner.titulo = Cuestionario!.Titulo!;
                //await _stateConteiner.SetCuestionario(Cuestionario, codigo);
                bool result = false;
                if (result)
                {
                    navigationManager.NavigateTo($"/Lobby/{codigo}");
                }
            }
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
        public async Task GetUsers(int code) => await _hubConnection.SendAsync("GetUsersByRoom", code);       
    }
}
