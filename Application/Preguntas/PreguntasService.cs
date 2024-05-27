using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Extensions;
using System.Net.Http.Json;

namespace anskus.Application.Preguntas
{
    public class PreguntasService : IPreguntasService
    {
        private readonly HttpClientServices _httpClientServices;
        public PreguntasService(HttpClientServices httpClientServices)
        {
            _httpClientServices = httpClientServices;
        }
        private async Task<HttpClient> PrivateClient() => await _httpClientServices.GetPrivateClient();
        public async Task<int> Add(PreguntasDTO pregunta)
        {
            var result = await (await PrivateClient()).PostAsJsonAsync(Constant.PreguntasRoute, pregunta);
            var response =await result.Content.ReadFromJsonAsync<int>();
            return response;

        }
       public async Task<bool> UpdatePregunta(PreguntasDTO preguntas)
        {
            var result = await (await PrivateClient()).PutAsJsonAsync(Constant.PreguntasRoute, preguntas);
            var response = await result.Content.ReadFromJsonAsync<bool>();
            return response;
        }

        public Task<PreguntasResponse> GetPreguntas(int pregunta)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletePregunta(int idpregunta)
        {

            var result = await (await PrivateClient()).DeleteAsync($"{Constant.PreguntasRoute}/{idpregunta}");
            var response = await result.Content.ReadFromJsonAsync<bool>();
            return response;
        }
    }

}
