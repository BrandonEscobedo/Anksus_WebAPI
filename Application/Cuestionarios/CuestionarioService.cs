using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Anksus_WebAPI.Models.dbModels;
using anskus.Application.Cuestionarios.Create;
using anskus.Application.Data;
using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Extensions;
using MediatR;
namespace anskus.Application.Cuestionarios
{
    public class CuestionarioService:ICuestionarioService
    {
        private readonly HttpClientServices _httpClientServices;
        private readonly ISender _mediator;
        public CuestionarioService(HttpClientServices httpClientServices, ISender mediator)
        {
            _httpClientServices = httpClientServices;
            _mediator = mediator;
        }
        private async Task<HttpClient> PrivateClient() => (await _httpClientServices.GetPrivateClient());
        private static string CheckResponseStatus(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return $"Ha ocurrido un error inesperado";
            else
                return null!;
        }
        private static CuestionarioResponse ErrorOperation(string message) => new(0, message, false);
        public async Task<int> Add(CuestionarioDTO cuestionario )
        {
            var response = await _mediator.Send(new CreateCuestionarioCommand(cuestionario));
            return response;
            //var result =await (await PrivateClient()).PostAsJsonAsync(Constant.CuestionarioController, cuestionario);

            //if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            //return await result.Content.ReadFromJsonAsync<CuestionarioResponse>() ?? throw new Exception("No se encontro nada");

        }
        public async Task<CuestionarioResponse> Update(CuestionarioDTO cuestionario)
        {
            var result = await (await PrivateClient()).PutAsJsonAsync(Constant.CuestionarioController, cuestionario);
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<CuestionarioResponse>() ?? throw new Exception("Hubo un error al aplicar los cambios");
        }



    }
}
