using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Extensions;
using System.Net.Http.Json;

namespace anskus.Application.Cuestionarios
{
    public class PreguntasService : IPreguntasService
    {
        private readonly HttpClientServices _httpClientServices;
        public PreguntasService(HttpClientServices httpClientServices)
        {
            _httpClientServices = httpClientServices;
        }
        private async Task<HttpClient> PrivateClient() => (await _httpClientServices.GetPrivateClient());
        private static string CheckResponseStatus(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return $"Ha ocurrido un error inesperado";
            else
                return null!;
        }
        private static PreguntasResponse ErrorOperation(string message) => new(null, false, message);
        public async Task<PreguntasResponse> Add(PreguntasDTO pregunta)
        {
            var result = await (await PrivateClient()).PostAsJsonAsync(Constant.AddPregunta, pregunta);

            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<PreguntasResponse>() ?? throw new Exception("No se encontro nada");
        }
        public Task<PreguntasResponse> GetPreguntas(int pregunta)
        {
            throw new NotImplementedException();
        }

        public Task<PreguntasResponse> DeletePregunta(int pregunta)
        {
            throw new NotImplementedException();
        }
    }

}
