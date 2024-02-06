using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TestAnskus.Client;
using TestAnskus.Client.Services.Implementacion;
using TestAnskus.Client.Services.Interfaces;
using MudBlazor.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using TestAnskus.Client.Utility;
using TestAnskus.Client.Services.Interfaces.Autenticacion;
using TestAnskus.Client.Services.Implementacion.Autenticacion;
using TestAnskus.Client.Services.Interfaces.CuestionarioActivo;
using TestAnskus.Client.Services.Implementacion.CuestionarioActivo;
using TestAnskus.Client.Services.Interfaces.Hub;
using TestAnskus.Client.Services.Implementacion.Hub;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7150/") });
//builder.Services.AddOidcAuthentication(options =>
//{
//    builder.Configuration.Bind("Local", options.ProviderOptions);
//});
builder.Services.AddScoped<ICuestionariosService, CuestionariosService>();
builder.Services.AddScoped<ICategoriaService, CategoriasService>();
builder.Services.AddScoped<IPreguntasService, PreguntasService>();
builder.Services.AddScoped<IIdContainer,IdConteiner>();
builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IRespuestasService,RespuestasService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<ICuestionarioAService,CuestionarioAService>();
builder.Services.AddScoped<IHubConnectionService, HubConnecionService>();
await builder.Build().RunAsync();
