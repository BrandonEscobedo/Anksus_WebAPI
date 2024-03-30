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
    public partial class Lobby
    {
        [Parameter]
        public int codigo { get; set; }
        private string mensaje = "";
        private List<CuestionarioDTO> cuestionario= new();
        
        private IReadOnlyList<ParticipanteEnCuestDTO> participantesActivos;

        protected override async Task OnInitializedAsync()
        {

            var response = await JSRuntime.InvokeAsync<string> ("sessionStorage.getItem", "Llave");
            if (!string.IsNullOrEmpty(response))
            {
               cuestionario = System.Text.Json.JsonSerializer.Deserialize<List<CuestionarioDTO>>(response) ?? throw  new Exception("No existe el cuestionario");
             
            }
            await HubServices.GetUsers(codigo);
            HubServices.NewParticipante += HandleruserJoin;
            HubServices.removeParticipante += HandlerUserLeft;
            participantesActivos = HubServices.ParticipantesActivos;
        }

        async Task ShowConfirmation()
        {
            DialogService di = new DialogService();
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
        public async ValueTask DisposeAsync()
        {
         bool confirmed=   await JSRuntime.InvokeAsync<bool>("confirmClose","Si sales ahora la sesión se eliminara.");
            if(confirmed==true)
            {
                HubServices.NewParticipante -= HandleruserJoin;
                HubServices.removeParticipante -= HandlerUserLeft;
                await HubServices.RemoveRoom(codigo);
            }
            else { return ; }
            

        }
  
    }
}
