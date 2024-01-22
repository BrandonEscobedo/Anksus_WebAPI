using Anksus_WebAPI.Models.DTO;
using System.Net.Http.Json;
using TestAnskus.Client.Services.Interfaces;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion
{
    public class RespuestasService : IRespuestasService
    {
        public readonly HttpClient _httpClient;
        public RespuestasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> CreateRespuestas(List<RespuestasDTO> respuestasDTO)
        {          
            var result =await _httpClient.PostAsJsonAsync("api/Respuestas/", respuestasDTO);
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
    }
}
