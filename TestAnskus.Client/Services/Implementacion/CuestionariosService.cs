using Anksus_WebAPI.Models.DTO;
using System.Net.Http.Json;
using TestAnskus.Client.Services.Interfaces;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion
{

    public class CuestionariosService : ICuestionariosService
    {
        private readonly HttpClient _httpClient;
        public CuestionariosService(HttpClient httpClient) 
        {
        _httpClient = httpClient;
        }
        public async Task<int> CreateCuestionario(CuestionarioDTO cuestionario)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Cuestionarios/", cuestionario);
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

        public Task<bool> DeleteCuestionario(int id)
        {
            throw new NotImplementedException();
        }

        public  async Task<int> EditCuestionario( CuestionarioDTO cuestionarioDTO)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Cuestionarios/{cuestionarioDTO.IdCuestionario}", cuestionarioDTO);
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

        public Task<ResponseAPI<List<CuestionarioDTO>>> GetCuestionario()
        {
            throw new NotImplementedException();
        }
    }
}
