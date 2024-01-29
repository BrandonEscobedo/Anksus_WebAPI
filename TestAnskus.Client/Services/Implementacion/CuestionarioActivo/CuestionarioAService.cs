using System.Net.Http.Json;
using TestAnskus.Client.Services.Interfaces.CuestionarioActivo;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion.CuestionarioActivo
{
    public class CuestionarioAService : ICuestionarioAService
    {
        private readonly HttpClient _httpClient;
        public CuestionarioAService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseAPI<int>> ActivarCuestionario(int id)
        {
            var result = await _httpClient.PostAsJsonAsync("api/CuestionarioActivo/",id);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (response!.EsCorrecto)
            {
                return new ResponseAPI<int> { EsCorrecto=true,Valor=response.Valor};
            }
            {
                return new ResponseAPI<int> { EsCorrecto = true, Valor = response.Valor };
            }

        }

        public Task<int> FinalizarCuestionario(int id)
        {
            throw new NotImplementedException();
        }
    }
}
