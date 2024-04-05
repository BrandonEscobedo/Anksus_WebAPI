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
        private bool mostrarComponenteHijo = false;

        private List<CuestionarioDTO> cuestionario= new();
        protected override async Task OnInitializedAsync()
        {
            cuestionario = _StateContainer.GetCuestionario();      
        }
        private void MostrarComponenteHijo()
        {
            mostrarComponenteHijo = true;
        }
        private void  ContadorFinalizadoHandler()
        {
            mensaje = "Listo";
        }
        private void Iniciar()
        {
           
        }

        public void Dispose()
        {
         HubServices.RemoveRoom(codigo);
        }
    }
}

