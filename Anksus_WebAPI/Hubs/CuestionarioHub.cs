using Anksus_WebAPI.Controllers;
using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;
using Anksus_WebAPI.Server.Hubs.Implementacion;
using Anksus_WebAPI.Server.Servicios;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using TestAnskus.Shared;

namespace Anksus_WebAPI.Server.Hubs
{
    public class CuestionarioHub:Hub<InotificationClient>
    {       
        private readonly TestAnskusContext _context;
        
        private static ConcurrentDictionary<string, List<ParticipanteEnCuestDTO>> SalaUsuario = new();
        public CuestionarioHub(TestAnskusContext context  )
        {
            _context = context;
        }
         
        public async Task CreateRoom(string code)
        {
            if(!SalaUsuario.ContainsKey(code))
            {
                SalaUsuario[code] = new List<ParticipanteEnCuestDTO>();
            }
            var codigo = Context.GetHttpContext()!.Request.Query["id"];
            await Groups.AddToGroupAsync(Context.ConnectionId, code);
            await Clients.Group(code).UsuariosEnLaSala(SalaUsuario[code.ToString()].Count);
        }
        public async Task<bool> AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            bool result = false;
            if (!SalaUsuario[participante.codigo.ToString()].Where(x => x.Nombre == participante.Nombre).Any())
            {
                SalaUsuario[participante.codigo.ToString()].Add(participante);
                await Clients.Group(participante.codigo.ToString()).NewParticipante(participante);
               
                var a = Clients.Group(participante.codigo.ToString()).CountUsers(participante.codigo);
                result=  true;
            }
            return result;

        }

        public async Task RemoveRoom(int code)
        {
             SalaUsuario.TryRemove(code.ToString(),out _);
            var cuest = _context.CuestionarioActivos.Where(x => x.Codigo == code).FirstOrDefault();
             _context.CuestionarioActivos.Remove(cuest!);
            await _context.SaveChangesAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            
            await  base.OnDisconnectedAsync(exception);
        }
        public async Task CountUsersByRoom(int code)
        {
            await Clients.Group(code.ToString()).CountUsers(code);
        }
        
     
             
    }
}
public interface InotificationClient
    {
    Task NewParticipante(ParticipanteEnCuestDTO participante);
    Task UsuariosEnLaSala(int count);
    Task getUsers(int code);
    Task CountUsers(int code);

}
