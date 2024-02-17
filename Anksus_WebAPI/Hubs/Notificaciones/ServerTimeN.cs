
using Anksus_WebAPI.Models.dbModels;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Anksus_WebAPI.Server.Hubs.Notificaciones
{
    public class ServerTimeN : BackgroundService,IServerTImeServices
    {
        private readonly ILogger<ServerTimeN> _logger;
        private readonly IHubContext<CuestionarioHub,InotificationClient>_context;
        private Timer _timer;
        public ServerTimeN(ILogger<ServerTimeN> logger, IHubContext<CuestionarioHub,InotificationClient> context)
        {
            _context = context;
            _logger = logger;
        }
        public void Star(int code)
        {
            _timer = new Timer(async _ =>
            {
                await _context.Clients.Group(code.ToString()).MensajePrueba("Mensaje");
            } , null,TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }
        public void Stop()
        {
            _timer?.Change(Timeout.Infinite, 0);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
        


        }
    }
}
