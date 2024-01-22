using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriasDTO>> GetAllCategorias();
    }
}
