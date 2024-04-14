using Anksus_WebAPI.Server.Hubs;
using Anksus_WebAPI.Server.Hubs.Notificaciones;
using anskus.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddHostedService<ServerTimeN  >();
builder.Services.AddSingleton<IServerTImeServices, ServerTimeN>();
builder.Services.AddStackExchangeRedisCache(redisOp =>
{
    var connection = builder.Configuration
    .GetConnectionString("Redis");
    redisOp.Configuration = connection;
});
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.MapHub<CuestionarioHub>("ChatCuest");
app.UseAuthorization();
app.MapControllers();
app.Run();
