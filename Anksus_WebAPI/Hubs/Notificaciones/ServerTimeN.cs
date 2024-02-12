
using Anksus_WebAPI.Models.dbModels;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Anksus_WebAPI.Server.Hubs.Notificaciones
{
    public class ServerTimeN : BackgroundService
    {
        private static readonly TimeSpan Period=TimeSpan.FromSeconds(5);
        private readonly ILogger<ServerTimeN> _logger;
        public ServerTimeN(ILogger<ServerTimeN> logger, IHubContext<CuestionarioHub,InotificationClient> context)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
        


        }
    }
}
