using Microsoft.AspNetCore.Components;
using anskus.Application.DTOs.Cuestionarios;
using MudBlazor;
namespace TestAnskus.Client.Pages.CrearEditarCuestionarios
{
   public partial class CrearCuestionario
    {
        [Parameter]
        public int id { get; set; } = 0;
       
        List<PreguntasDTO> ListaPreguntas { get; set; } = new();
        List<RespuestasDTO> ListaRespuestasDTOs { get; set; } = new List<RespuestasDTO>();
        public PreguntasDTO preguntasDTO { get; set; } = new PreguntasDTO();
        private void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            DialogService.Show<MdCrearCuestionario>("Simple Dialog", options);
        }
        protected override async Task OnInitializedAsync()
        {
            _IdConteiner.idCuestionario = id;
            ListaRespuestasDTOs.Add(new RespuestasDTO
            {
                IdPregunta = 0,
                respuesta = "",
                RCorrecta = false
            });
        }
        public async Task GuardarEditarPregunta()
        {
            var nuevapregunta = new PreguntasDTO()
            {
                
                IdCuestionario = _IdConteiner.idCuestionario,
                pregunta = preguntasDTO.pregunta
            };
            preguntasDTO.IdCuestionario = _IdConteiner.idCuestionario;
            var idpreg = await _preguntasService.Add(nuevapregunta);
            if (idpreg != 0)
            {
                
                _IdConteiner.SetIdPregunta(idpreg);
                nuevapregunta.IdPregunta = idpreg;
                ListaPreguntas.Add(nuevapregunta);
                await GuardarRespuestas();
                await CleanRespuestas();
                preguntasDTO = new();
            }
        }
        public void EliminarRespuesta(RespuestasDTO respuesta)
        {
            ListaRespuestasDTOs.Remove(respuesta);
        }
        public async Task EliminarPregunta(PreguntasDTO pregunta)
        {
            var result = await _preguntasService.DeletePregunta(pregunta.IdPregunta);
            if (result)
            {
                ListaPreguntas.Remove(pregunta);
            }

        }
        public async Task AgregarRespuesta()
        {
            ListaRespuestasDTOs.Add(new RespuestasDTO
            {
                IdPregunta = 0,
                respuesta = "",
                RCorrecta = false
            });
        }
        public async Task CleanRespuestas()
        {
            ListaRespuestasDTOs.Clear();
            ListaRespuestasDTOs.Add(new RespuestasDTO
            {
                IdPregunta = 0,
                respuesta = "",
                RCorrecta = false
            });

        }
        public async Task CambiarID(int id)
        {
            foreach (var respuesta in ListaRespuestasDTOs)
            {
                respuesta.IdPregunta = id;
            }
        }
        public async Task GuardarRespuestas()
        {
            await CambiarID(_IdConteiner.idPregunta);
            ListaRespuestasDTOs.RemoveAll(r => r.respuesta.Length == 0);
            await respuestasService.Add(ListaRespuestasDTOs);
        }

    }
}
