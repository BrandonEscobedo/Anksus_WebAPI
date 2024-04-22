using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.Extensions;
using System.Net.Http.Json;

namespace anskus.Application.Respuestas
{
    public class RespuestasServices(HttpClientServices httpClientServices) : IRespuestasServices
    {
        private async Task<HttpClient> PrivateClient() => (await httpClientServices.GetPrivateClient());
        public async Task<bool> Add(List<RespuestasDTO> respuestas)
        {
            var  result = await (await PrivateClient()).PostAsJsonAsync(Constant.RespuestasRoute, respuestas);
            var response = await result.Content.ReadFromJsonAsync<bool>();
            return response;
        }
    }

}
