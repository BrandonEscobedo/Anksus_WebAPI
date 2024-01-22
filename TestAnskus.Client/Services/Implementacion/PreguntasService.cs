using Anksus_WebAPI.Models.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using TestAnskus.Client.Services.Interfaces;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion
{
    public class PreguntasService : IPreguntasService
    {
        private readonly HttpClient _httpClient;
        public PreguntasService(HttpClient httpClient)
        {

            _httpClient = httpClient;
        }
        public async Task<int> CreatePregunta(PreguntasDTO preguntasDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Preguntas/", preguntasDTO);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.mensaje);
            }
        }

        public async Task<bool> DeletePregunta(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Preguntas/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (response!.EsCorrecto)
            {
                return response.EsCorrecto!;
            }
            else
            {
                throw new Exception(response.mensaje);
            }
        }

        public Task<bool> EditarPregunta(PreguntasDTO preguntaDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<PreguntasDTO> GetPreguntas(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<PreguntasDTO>>($"api/Preguntas/{id}");
            
            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.mensaje);
            }

        }
    }
}
