
using Anksus_WebAPI.Models.dbModels;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Anksus_WebAPI.Server.Hubs.Notificaciones
{
    public class ServerTimeN : BackgroundService
    {
        private static readonly TimeSpan Period=TimeSpan.FromSeconds(5);
        private readonly ILogger<ServerTimeN> _logger;
        private readonly IHubContext<CuestionarioHub, InotificationClient> _context;
        public ServerTimeN(ILogger<ServerTimeN> logger, IHubContext<CuestionarioHub,InotificationClient> context)
        {
            _logger = logger;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(Period);
            while (!stoppingToken.IsCancellationRequested && 
                    await timer.WaitForNextTickAsync(stoppingToken))
            {
                var datetime = DateTime.Now;
                _logger.LogInformation("Executing {Service} {Time}",nameof(ServerTimeN),datetime);
                await _context.Clients.User(ClaimTypes.Name)
                    .receiveNotificacion($"Server time ={datetime}");
            }
        }
    }
}
