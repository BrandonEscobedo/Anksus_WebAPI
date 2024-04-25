using anskus.Application.CuestionarioActivo;
using anskus.Application.Cuestionarios;
using anskus.Application.Extensions;
using anskus.Application.HubSignalr;
using anskus.Application.HubSignalr.StateContainerHub;
using anskus.Application.Preguntas;
using anskus.Application.Respuestas;
using anskus.Application.Services;
using anskus.Application.Utility;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetcodeHub.Packages.Extensions.LocalStorage;
using System.Reflection;

namespace anskus.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddAuthorizationCore();
            services.AddNetcodeHubLocalStorageService();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());         
            services.AddScoped<Extensions.LocalStorageServices>();
            services.AddScoped<HttpClientServices>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();
            services.AddScoped<ICuestionarioService, CuestionarioService>();
            services.AddScoped<ICuestionarioActivoService, CuestionarioActivoService>();
            services.AddScoped<IHubconnectionService, HubconnectionService>();
            services.AddScoped<IIdContainer, IdContainer>();
            services.AddScoped<IStateConteiner, StateConteiner>();
            services.AddScoped<IPreguntasService, PreguntasService>();
            services.AddScoped<IRespuestasServices, RespuestasServices>();
            services.AddTransient<CustomHttpHandler>();
            services.AddCascadingAuthenticationState();
            services.AddHttpClient("TestAnskusClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7150/");
            }).AddHttpMessageHandler<CustomHttpHandler>();

            return services;
        }
    }
}
