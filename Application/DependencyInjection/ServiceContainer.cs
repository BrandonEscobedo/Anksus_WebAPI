using anskus.Application.Extensions;
using anskus.Application.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetcodeHub.Packages.Extensions.LocalStorage;

namespace anskus.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddAuthorizationCore();
            services.AddNetcodeHubLocalStorageService();
            services.AddScoped<Extensions.LocalStorageServices>();
            services.AddScoped<HttpClientServices>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();
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
