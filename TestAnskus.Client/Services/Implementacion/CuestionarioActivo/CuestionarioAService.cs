using Anksus_WebAPI.Models.DTO;
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
        public async Task<ResponseAPI<CuestionarioActivoDTO>> ActivarCuestionario(int idcuestionario)
        {
            var result = await _httpClient. 
            PostAsJsonAsync($"/api/CuestionarioActivo?idcuestionario={idcuestionario}", new {});
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<CuestionarioActivoDTO>>();
            if (response!.EsCorrecto)
            {
                
                return new ResponseAPI<CuestionarioActivoDTO> {EsCorrecto=true,Valor=response.Valor };
            }
            else
            {
                return new ResponseAPI<CuestionarioActivoDTO> { EsCorrecto = false, Valor = response.Valor };
            }

        }

        public Task<ResponseAPI<CuestionarioActivoDTO>> FinalizarCuestionario(int id)
        {
            throw new NotImplementedException();
        }
    }
}
