using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Anksus_WebAPI.Models.dbModels;
using anskus.Application.Data;
using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.Extensions;
using anskus.Domain.CuestionariosInterface;
namespace anskus.Application.Cuestionarios
{
    public class CuestionarioService:ICuestionarioService
    {
        private readonly HttpClientServices _httpClientServices;
        public CuestionarioService( HttpClientServices httpClientServices)
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
        public async Task<GeneralResponse> Add(CuestionarioDTO cuestionario )
        {
            var result =await (await PrivateClient()).PostAsJsonAsync(Constant.AddCuestionario, cuestionario);
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>() ?? throw new Exception("No se encontro nada");

        }

    }
}
