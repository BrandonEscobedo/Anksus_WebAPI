using Anksus_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Extensions;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TestAnskus.Shared;

namespace TestAnskus.Client.Pages.EnJuego.Creador
{
    public partial class Lobby: IDisposable
    {
        [Parameter]
        public int codigo { get; set; }
        private string mensaje = "";
        private List<CuestionarioDTO> cuestionario= new();

        private IReadOnlyList<ParticipanteEnCuestDTO>? participantesActivos;

        protected override async Task OnInitializedAsync()
        {
            cuestionario = _StateContainer.GetCuestionario();
            await HubServices.GetUsers(codigo);
            HubServices.NewParticipante += HandleruserJoin;
            HubServices.removeParticipante += HandlerUserLeft;
            participantesActivos = HubServices.partipantesEnCuestionario;
        }


     private void Iniciar()
        {
            navigationManager.NavigateTo("/Iniciando");
        }
        private void HandleruserJoin(ParticipanteEnCuestDTO participante)
        {
           
            participantesActivos = participantesActivos.Append(participante).ToList();
            StateHasChanged();
        }

        private void HandlerUserLeft(ParticipanteEnCuestDTO participante)
        {
            participantesActivos = participantesActivos.Where(p => p.Nombre != participante.Nombre).ToList();
            StateHasChanged();
        }



        public void Dispose()
        {
         HubServices.RemoveRoom(codigo);
        }
    }
}
