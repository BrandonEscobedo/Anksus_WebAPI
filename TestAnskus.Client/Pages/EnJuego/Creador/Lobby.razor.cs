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
        private CuestionarioDTO cuestionario= new();
        protected override void OnInitialized()
        {
            cuestionario = _StateContainer.GetCuestionario();
            _StateContainer.OnSiguientePregunta +=OnsiguientePregunta;
        }
        private void  OnsiguientePregunta(PreguntasDTO pregunta, string? titulo)
        {
            _StateContainer.pregunta = pregunta;
            if (!string.IsNullOrEmpty(titulo))
            {       
                navigationManager.NavigateTo("/Titulo/Creador");
            }
        }
        private async Task Iniciar()
        {
          await  HubServices.SiguientePreguntaUsuarios();
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

