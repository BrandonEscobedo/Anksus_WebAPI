using anskus.Application.DTOs.Cuestionarios;

namespace TestAnskus.Client.Services.Interfaces.Hub
{
    public interface IHubConnectionService
    {
        Task<bool> VerificarCodigo(int code);
    }
}
