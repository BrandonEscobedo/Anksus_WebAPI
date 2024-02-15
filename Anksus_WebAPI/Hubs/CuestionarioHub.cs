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
        private static ConcurrentDictionary<int, List<ParticipanteEnCuestDTO>> SalaUsuario = new();
        public CuestionarioHub(TestAnskusContext context  )
        {
            _context = context;
        }    
        public async Task CreateRoom(int code)
        {
            if(!SalaUsuario.ContainsKey(code))
            {
                SalaUsuario[code] = new List<ParticipanteEnCuestDTO>();
            }
            var codigo = Context.GetHttpContext()!.Request.Query["id"];
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
        public async Task UserLeftRoom(ParticipanteEnCuestDTO participante)
        {
            if (SalaUsuario.ContainsKey(participante.codigo))
            {
                SalaUsuario[participante.codigo].Remove(participante);
            }
            await Clients.Group(participante.codigo.ToString()).RemoveUser(participante);

        }
        public async Task RemoveRoom(int code)
        {
             SalaUsuario.TryRemove(code,out _);
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            
            await  base.OnDisconnectedAsync(exception);
        }

    }
}
public interface InotificationClient
    {
    Task RemoveUser(ParticipanteEnCuestDTO participante);
    Task NewParticipante(ParticipanteEnCuestDTO participante);
    Task UsuariosEnLaSala(List<string> usuarios);
    Task getUsers(int code);

}
