using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TestAnskus.Client.Services.Interfaces.Autenticacion;
using TestAnskus.Client.Utility;
using TestAnskus.Shared.AutorizacionDTO;

namespace TestAnskus.Client.Services.Implementacion.Autenticacion
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;
        public AuthService(HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
        }
        public async Task<RegisterResult> Register(RegisterModelA registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Account", registerModel);
            if (!result.IsSuccessStatusCode)
            {
             return new RegisterResult { Successful = false, Errors = new List<string> { "Ocurrio un error Al registrar el usuario." } };
            }
            return new RegisterResult { Successful = true, Errors = new List<string> { "Cuenta Creada Correctamente" } }; 
                
        }
        public async Task<LoginResult> Login(LoginModelA loginModel)
        {
            var loginAsJson =JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/Login",
                new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginresult=JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive=true});
            if (!response.IsSuccessStatusCode)
            {
                return loginresult!;
            }
            await _localStorageService.SetItemAsync("authToken", loginresult!.Token);
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginresult.Token);
            return loginresult!;
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsloggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        
        }

       
    }
}
