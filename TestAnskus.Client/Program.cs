using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TestAnskus.Client;
using MudBlazor.Services;
using Microsoft.AspNetCore.SignalR.Client;
using anskus.Application.DependencyInjection;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddApplicationService();
builder.Services.AddMudServices();
//builder.Services.AddScoped<ICategoriaService, CategoriasService>();

builder.Services.AddScoped(sp =>
{
    return new HubConnectionBuilder()
   .WithUrl("https://localhost:7150/ChatCuest")
   .WithAutomaticReconnect()
   .Build();

});
await builder.Build().RunAsync();
