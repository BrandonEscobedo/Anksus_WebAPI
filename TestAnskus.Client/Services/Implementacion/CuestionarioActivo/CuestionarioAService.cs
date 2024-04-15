using System.Net.Http.Json;
using TestAnskus.Client.Services.Interfaces.CuestionarioActivo;
using anskus.Application.DTOs.Cuestionarios;

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
        public async Task<bool> VerificarCodigo(int code)
        {
            var result = await _httpClient.
            GetFromJsonAsync<bool>($"api/ParticipantesEnCuestionario/{code}");
            return result;
        }
        public Task<ResponseAPI<CuestionarioActivoDTO>> FinalizarCuestionario(int id)
        {
            throw new NotImplementedException();
        }
    }
}
