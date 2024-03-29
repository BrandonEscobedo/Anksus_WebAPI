using Anksus_WebAPI.Controllers;
using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;
using Anksus_WebAPI.Server.Hubs.Implementacion;
using Anksus_WebAPI.Server.Hubs.Notificaciones;
using Anksus_WebAPI.Server.Servicios;
using Anksus_WebAPI.Server.Utilidades;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using System.Text.Json;
using TestAnskus.Shared;
namespace Anksus_WebAPI.Server.Hubs
{
    public class CuestionarioHub : Hub<InotificationClient> 
    {
        private readonly TestAnskusContext _context;
        private readonly IDistributedCache _distributedCache;
        //private static ConcurrentDictionary<int, List<ParticipanteEnCuestDTO>> SalaUsuario = new();
        public CuestionarioHub(TestAnskusContext context , IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
        }
        public async Task<bool> CreateRoom(string code)
        {
            await _distributedCache.CreateRoomCache(code);
            await Groups.AddToGroupAsync(Context.ConnectionId, code.ToString());
            return true;
        }
        public async Task<bool> AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            bool resultado = await _distributedCache.AddUserToRoomCache(participante.codigo.ToString(), participante);

            if (resultado)
            {
                await Clients.Group(participante.codigo.ToString()).NewParticipante(participante);

            }
            return resultado;

        }
        public async Task GetUsersByRoom(int code)
        {

            await Clients.Group(code.ToString()).getUsers(code);
        }
        public async Task UserLeftRoomUserLeftRoom(ParticipanteEnCuestDTO participante)
        {
            //if (SalaUsuario.ContainsKey(participante.codigo))
            //{
            //    var result = SalaUsuario[participante.codigo]
            //            .FirstOrDefault(x => x.IdPeC == participante.IdPeC);
            //    if (result != null)
            //    {
            //        SalaUsuario[participante.codigo].Remove(result);
            //        await Clients.Group(participante.codigo.ToString()).RemoveUser(participante);

            //    }
            //}

        }
        public async Task RemoveRoom(int code)
        {
            //bool result = SalaUsuario.TryRemove(code, out _);
            //if (result)
            //{
            //    var cuestionario = _context.CuestionarioActivos.Where(x => x.Codigo == code).FirstOrDefault();
            //    if (cuestionario != null)
            //    {
            //        _context.CuestionarioActivos.Remove(cuestionario);
            //        await _context.SaveChangesAsync();
            //    }
            //}
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {

            await base.OnDisconnectedAsync(exception);
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
