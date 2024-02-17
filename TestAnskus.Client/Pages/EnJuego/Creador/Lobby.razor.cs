using Microsoft.AspNetCore.Components;
using TestAnskus.Shared;

namespace TestAnskus.Client.Pages.EnJuego.Creador
{
    public partial class Lobby
    {
        [Parameter]
        public int codigo { get; set; }
        private string mensaje = "";

        private IReadOnlyList<ParticipanteEnCuestDTO> participantesActivos;

        protected override async Task OnInitializedAsync()
        {
            await HubServices.GetUsers(codigo);
            HubServices.NewParticipante += HandleruserJoin;
            HubServices.removeParticipante += HandlerUserLeft;
            HubServices.Mensajerecibido += ActualizarMensaje;
            participantesActivos = HubServices.ParticipantesActivos;
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
        private void ActualizarMensaje(string msg)
        {
            mensaje = msg;
            StateHasChanged();
        }
        private async Task IniciarTarea(int code)
        {
            await HubServices.IniciarTarea(code);

        }
        public async ValueTask DisposeAsync()
        {
            HubServices.NewParticipante -= HandleruserJoin;
            HubServices.removeParticipante -= HandlerUserLeft;
            await HubServices.RemoveRoom(codigo);
        }
    }
}
