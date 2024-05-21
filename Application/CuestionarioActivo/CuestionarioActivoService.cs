using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.Extensions;
using System.Net.Http.Json;
namespace anskus.Application.CuestionarioActivo
{
    public class CuestionarioActivoService(HttpClientServices httpClientServices) : ICuestionarioActivoService
    {
        private async Task<HttpClient> PrivateClient() => (await httpClientServices.GetPrivateClient());
        public async Task<CuestionarioActivoDTO> ActivarCuestionario(int  idcuestionario)
        {
            
            var response = await (await PrivateClient()).PostAsync($"{Constant.CuestionarioActivoRoute}?idcuest={idcuestionario}",null);
            var result= await response.Content.ReadFromJsonAsync<CuestionarioActivoDTO>();
            return result;
        }
        public async Task<bool> VerificarCodigoEntrar(int code)
        {
           var response = await (await PrivateClient())
                .GetFromJsonAsync<bool>($"{Constant.CuestionarioActivoRoute}?codigo={code}");         
            return response;
        }

    }
}
