using TestAnskus.Shared.AutorizacionDTO;

namespace TestAnskus.Client.Services.Interfaces.Autenticacion
{
    public interface IAuthService
    {
        Task<RegisterResult> Register(RegisterModelA registerModel);
        Task<LoginResult> Login(LoginModelA loginModel);
        Task Logout();
    }
}
