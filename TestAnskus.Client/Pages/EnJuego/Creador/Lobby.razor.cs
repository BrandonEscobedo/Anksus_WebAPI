using Microsoft.AspNetCore.Components;
using TestAnskus.Shared;

namespace TestAnskus.Client.Pages.EnJuego.Creador
{
    public partial class Lobby
    {
        [Parameter]
        public int code { get; set; }
     

        private IReadOnlyList<ParticipanteEnCuestDTO> participantesActivos;

        protected override async Task OnInitializedAsync()
        {
            await HubServices.GetUsers(code);
            HubServices.NewParticipante += HandleruserJoin;
            HubServices.removeParticipante += HandlerUserLeft;
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

        public async ValueTask DisposeAsync()
        {
            HubServices.NewParticipante -= HandleruserJoin;
            HubServices.removeParticipante -= HandlerUserLeft;
        }
    }
}
