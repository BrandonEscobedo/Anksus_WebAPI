using Anksus_WebAPI.Controllers;
using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;
using Anksus_WebAPI.Server.Hubs.Implementacion;
using Anksus_WebAPI.Server.Hubs.Notificaciones;
using Anksus_WebAPI.Server.Servicios;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using TestAnskus.Shared;

namespace Anksus_WebAPI.Server.Hubs
{
    public class CuestionarioHub:Hub<InotificationClient>
    {       
        private readonly TestAnskusContext _context;
        private readonly IServerTImeServices _serverTimeN;
        private static ConcurrentDictionary<int, List<ParticipanteEnCuestDTO>> SalaUsuario = new();
        public CuestionarioHub(TestAnskusContext context, IServerTImeServices serverTimeN)
        {
            _context = context;
            _serverTimeN = serverTimeN;
        }
        public async Task CreateRoom(int code)
        {
            if(!SalaUsuario.ContainsKey(code))
            {
                SalaUsuario[code] = new List<ParticipanteEnCuestDTO>();
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, code.ToString());
        }   
        public async Task<bool> AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            bool resultado = false;
            if (!SalaUsuario[participante.codigo].Where(x=>x.Nombre==participante.Nombre).Any())
            {
                SalaUsuario[participante.codigo].Add(participante);
                await Clients.Group(participante.codigo.ToString()).NewParticipante(participante);
                resultado = true;
            }
            return resultado;

        }
        public async Task GetUsersByRoom(int code)
        {

           await  Clients.Group(code.ToString()).getUsers(code);
        }
        public async Task UserLeftRoomUserLeftRoom(ParticipanteEnCuestDTO participante)
        {
            if (SalaUsuario.ContainsKey(participante.codigo))
            {
            var result=   SalaUsuario[participante.codigo]
                    .FirstOrDefault(x=>x.IdPeC==participante.IdPeC);
                if (result != null)
                {
                    SalaUsuario[participante.codigo].Remove(result);
                    await Clients.Group(participante.codigo.ToString()).RemoveUser(participante);

                }
            }

        }
        public async Task RemoveRoom(int code)
        {
          bool result =   SalaUsuario.TryRemove(code,out _);
            if (result)
            {
               var cuestionario= _context.CuestionarioActivos.Where(x => x.Codigo == code).FirstOrDefault();
                if (cuestionario != null)
                {
                    _context.CuestionarioActivos.Remove(cuestionario);
                     await _context.SaveChangesAsync();
                }
            }
        }
        public async Task IniciarTarea(int code)
        {
            _serverTimeN.Star(code);
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            
            await  base.OnDisconnectedAsync(exception);
        }

    }
}
public interface InotificationClient
    {
    Task MensajePrueba(string mensaje); 
    Task RemoveUser(ParticipanteEnCuestDTO participante);
    Task NewParticipante(ParticipanteEnCuestDTO participante);
    Task UsuariosEnLaSala(List<string> usuarios);
    Task getUsers(int code);

}
