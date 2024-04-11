using Anksus_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using TestAnskus.Client.Services.Interfaces;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion.Hub
{
    public class HubConnecionService
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
            _hubConnection.On<int>("SiguientePregunta",OnSiguientePregunta );
            _hubConnection.On<List<PreguntasDTO>>("IniciarCuestionario", IniciarCuestionario);
            _hubConnection.On<ParticipanteEnCuestDTO>("RemoveUser", RemoveParticipante);
            this.navigationManager = navigationManager;
            _stateConteiner = stateConteiner;

        }
        private void IniciarCuestionario(List<PreguntasDTO> cuestionario)
        {
            _stateConteiner.IniciarCuestionario(cuestionario);

        }
        private void OnSiguientePregunta(int idpregunta)
        {
            _stateConteiner.SiguientePregunta(idpregunta);
        }
        public async Task SiguientePreguntaUsuarios( )
        {
            DatosPregunta datosPregunta = await _stateConteiner.IndiceSiguientePregunta();
            
            await _hubConnection.InvokeAsync("SiguientePregunta", datosPregunta.CodigoRoom);
       
        }
        private void OnNewParticipante(ParticipanteEnCuestDTO participante) => _stateConteiner.AddParticipante(participante);
        private void RemoveParticipante(ParticipanteEnCuestDTO participante) => _stateConteiner.RemoveParticipante(participante);
        public async Task NewRom(int codigo, int idcuestionario)
        {
            var Cuestionario = await _httpClient.
                   GetFromJsonAsync<List<CuestionarioDTO>>($"api/Cuestionarios/{idcuestionario}")
                   ?? throw new Exception("Error al encontrar el cuestionario");
            if (Cuestionario != null)
            {
                List<int> idpreguntas = new List<int>();
                foreach (var pregunta in Cuestionario)
                {
                  foreach(var pregunt in pregunta.Pregunta)
                    {
                        idpreguntas.Add(pregunt.IdPregunta);

                    }
                }
                bool result = await _hubConnection.
                    InvokeAsync<bool>("CreateRoom", codigo, idpreguntas);
                await _stateConteiner.SetCuestionario(Cuestionario, codigo);
                if (result)
                {
                    navigationManager.NavigateTo($"/Lobby/{codigo}");
                }
            }

        }
        
        public async Task IniciarCuestionarioByRoom(int codigo)
        {
            List<PreguntasDTO> preguntas = new();
            foreach (var preguntasDTO in _stateConteiner.Cuestionario)
            {
                preguntas = preguntasDTO.Pregunta.ToList();
            }
            await _hubConnection.InvokeAsync("IniciarCuestionario", codigo, preguntas);

            navigationManager.NavigateTo("/PreguntaActual");
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
        //public async Task UserLeft(ParticipanteEnCuestDTO participante) => await _hubConnection.SendAsync("UserLeftRoomUserLeftRoom", participante);
        //public void RemoveRoom(int codigo) => _hubConnection.SendAsync("RemoveRoom", codigo);

    }
}
