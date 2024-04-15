using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
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
        private static GeneralResponse ErrorOperation(string message) => new(false, message);
        public async Task<GeneralResponse> Add(PreguntasDTO preguntas)
        {
            var result = await (await PrivateClient()).PostAsJsonAsync(Constant.AddPregunta, preguntas);
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>() ?? throw new Exception("No se encontro nada");
        }
    }

}
