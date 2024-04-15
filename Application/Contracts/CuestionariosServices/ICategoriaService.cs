using anskus.Application.DTOs.Cuestionarios;

namespace TestAnskus.Client.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriasDTO>> GetAllCategorias();
    }
}
