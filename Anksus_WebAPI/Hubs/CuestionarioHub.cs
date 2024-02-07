using Anksus_WebAPI.Controllers;
using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;
using Microsoft.AspNetCore.SignalR;
using TestAnskus.Shared;

namespace Anksus_WebAPI.Server.Hubs
{
    public class CuestionarioHub:Hub<InotificationClient>
    {
        static int count = 0;
        private readonly TestAnskusContext _context;
     private static   List<ParticipanteEnCuestDTO> Participantes = new List<ParticipanteEnCuestDTO>();
        public CuestionarioHub(TestAnskusContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            count++;
            await Clients.All.UpdatesClientsCount(count);
            await Clients.Client(Context.ConnectionId).receiveNotificacion($"Thank you for connecting {Context.User?.Identity?.Name}");
            await base.OnConnectedAsync();
            
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            count--;
            await Clients.All.UpdatesClientsCount(count);
            await  base.OnDisconnectedAsync(exception);
        }
        public async Task Sendusuario(ParticipanteEnCuestDTO participante)
        {
            await Clients.All.receiveParticipante(participante);
        }
        public async Task IngresarUsuario(string participante,string codigo)
        {
            
            //ParticipanteEnCuestDTO p = new ParticipanteEnCuestDTO
            //{
                
            //    codigo=Context.ConnectionId,
            //    Nombre = participante
            //};
            // Participantes.Add(p);
            //await Groups.AddToGroupAsync(Context.ConnectionId, codigo );
            //await Clients.Group(codigo).UsuarioConectado( participante);
            //var TotalParticipantes = Participantes.Where(x => x.codigo == codigo).Select(u=>u.Nombre).ToList();
            //await Clients.Caller.UsuariosEnLaSala(TotalParticipantes);
        }
    }
}
public  interface InotificationClient
    {
    Task Sendusuario();
    Task UpdatesClientsCount(int count);
    Task receiveParticipante(ParticipanteEnCuestDTO participante);
    Task UsuariosEnLaSala(List<string> usuarios);
    Task UsuarioConectado(string nombre);
    Task ReceiveMensaje(string mensaje);
    Task receiveNotificacion(string messege);
}