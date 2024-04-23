using System.Net.Http.Json;
using anskus.Application.DTOs.Cuestionarios;

namespace TestAnskus.Client.Services.Implementacion
{
    public class CategoriasService /*ICategoriaService*/
    {
        private readonly HttpClient _httpClient;
        public CategoriasService (HttpClient httpClient) 
        {
        _httpClient = httpClient;
        }
        public async Task<List<CategoriasDTO>> GetAllCategorias()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<CategoriasDTO>>>("api/Categorias/");
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
