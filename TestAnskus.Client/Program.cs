using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TestAnskus.Client;
using TestAnskus.Client.Services.Implementacion;
using TestAnskus.Client.Services.Interfaces;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7150/") });
builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});
builder.Services.AddSweetAlert2();
builder.Services.AddScoped<ICuestionariosService, CuestionariosService>();
builder.Services.AddScoped<ICategoriaService, CategoriasService>();
builder.Services.AddScoped<IPreguntasService, PreguntasService>();
builder.Services.AddScoped<IIdContainer,IdConteiner>();

builder.Services.AddScoped<IRespuestasService,RespuestasService>();
await builder.Build().RunAsync();
