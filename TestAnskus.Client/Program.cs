using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TestAnskus.Client;
using TestAnskus.Client.Services.Implementacion;
using TestAnskus.Client.Services.Interfaces;
using MudBlazor.Services;
using TestAnskus.Client.Services.Interfaces.CuestionarioActivo;
using TestAnskus.Client.Services.Implementacion.CuestionarioActivo;
using TestAnskus.Client.Services.Implementacion.Hub;
using Microsoft.AspNetCore.SignalR.Client;
using anskus.Application.DependencyInjection;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddApplicationService();

builder.Services.AddMudServices();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7150/") });
//builder.Services.AddOidcAuthentication(options =>
//{
//    builder.Configuration.Bind("Local", options.ProviderOptions);
//});
builder.Services.AddScoped<ICuestionariosService, CuestionariosService>();
builder.Services.AddScoped<ICategoriaService, CategoriasService>();
builder.Services.AddScoped<IPreguntasService, PreguntasService>();
builder.Services.AddScoped<IIdContainer,IdConteiner>();
builder.Services.AddScoped<IRespuestasService,RespuestasService>();
builder.Services.AddScoped<ICuestionarioAService,CuestionarioAService>();
builder.Services.AddScoped<IStateConteiner,StateConteiner>();
builder.Services.AddScoped< HubConnecionService>();
builder.Services.AddScoped(sp =>
{
    return new HubConnectionBuilder()
   .WithUrl("https://lodcalhost:7150/ChatCuest")
   .WithAutomaticReconnect()
   .Build();

});
await builder.Build().RunAsync();
