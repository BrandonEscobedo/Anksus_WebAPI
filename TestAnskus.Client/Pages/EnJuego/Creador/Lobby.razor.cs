using Microsoft.AspNetCore.Components;
using TestAnskus.Shared;

namespace TestAnskus.Client.Pages.EnJuego.Creador
{
    public partial class Lobby
    {
        int cantidad = 0;
        [Parameter]
        public int code { get; set; }
        [Parameter]
        public string id { get; set; }
        [CascadingParameter]
        private ParticipanteEnCuestDTO? participante { get; set; }
        private List<ParticipanteEnCuestDTO> participantesActivos = new();
        protected override Task OnInitializedAsync()
        {
            HubServices.NewParticipante += HandleruserJoin;

            return base.OnInitializedAsync();
        }
        private void HandleruserJoin(ParticipanteEnCuestDTO participante)
        {
            participantesActivos.Add(participante);
            StateHasChanged();
        }
        public async ValueTask DisposeAsync()
        {

        }
    }
}
