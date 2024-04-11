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
        private List<CuestionarioDTO> cuestionario= new();
        protected override async Task OnInitializedAsync()
        {
            cuestionario = _StateContainer.GetCuestionario();      
        }
        private async Task Iniciar()
        {
          await  HubServices.IniciarCuestionarioByRoom(codigo);
        }
        private void Salir()
        {
            navigationManager.NavigateTo("#");
        }
     
        public void Dispose()
        {
        }
    }
}

