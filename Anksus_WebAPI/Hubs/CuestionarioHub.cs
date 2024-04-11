using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;
using Anksus_WebAPI.Server.Utilidades;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

using TestAnskus.Shared;
namespace Anksus_WebAPI.Server.Hubs
{
    public class CuestionarioHub : Hub<InotificationClient>
    {
        private readonly TestAnskusContext _context;
        private readonly IDistributedCache _distributedCache;
        public CuestionarioHub(TestAnskusContext context, IDistributedCache distributedCache)
        {
            _context = context;

            _distributedCache = distributedCache;
        }
        public async Task<bool> CreateRoom(int code, List<int> idpreg)
        {
            await _distributedCache.CreateRoomCache(code.ToString());
            Context.Items["Preguntas"] =idpreg;
            Context.Items["Codigo"] = code;
            await Groups.AddToGroupAsync(Context.ConnectionId, code.ToString());
            return true;
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            int? code = (int?)Context.Items["Codigo"];
            if (code != null && code != 0)
            {
                var cuestionario = _context.CuestionarioActivos.Where(x => x.Codigo == code).FirstOrDefault();
                if (cuestionario != null)
                {
                    _context.CuestionarioActivos.Remove(cuestionario);
                    await  _context.SaveChangesAsync();
                }
            }
            else
            {
                ParticipanteEnCuestDTO? participante = (ParticipanteEnCuestDTO?)Context.Items["UserData"];
                if(participante != null)
                {
                    await _distributedCache.RemoveUserFromRoom(participante.codigo.ToString(), participante);
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, participante.codigo.ToString());
                    await Clients.Group(participante.codigo.ToString()).RemoveUser(participante);
                }
            }
        }

        public async Task<bool> AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            bool resultado = await _distributedCache.AddUserToRoomCache(participante.codigo.ToString(), participante);

            if (resultado)
            {
                Context.Items["UserData"] = participante;

                await Clients.Group(participante.codigo.ToString()).NewParticipante(participante);
                await Groups.AddToGroupAsync(Context.ConnectionId, participante.codigo.ToString());
            }
            return resultado;

        }
        public async Task IniciarCuestionario(int Room, List<PreguntasDTO> cuestionario)
        {
            await Clients.Group(Room.ToString()).IniciarCuestionario(cuestionario);
        }
        public async Task SiguientePregunta(int Room)
        {

            List<int>? list = (List<int>?) Context.Items["Preguntas"];
            if (list != null)
            {
                await Clients.Group(Room.ToString()).SiguientePregunta(list.First());
                list.RemoveAt(0);
                Context.Items["Preguntas"] = list;
            }
        }



    }
}
public interface InotificationClient
{
    Task IniciarCuestionario(List<PreguntasDTO> cuestionarios);
    Task SiguientePregunta(int idpregunta);
    Task MensajePrueba(string mensaje);
    Task RemoveUser(ParticipanteEnCuestDTO participante);
    Task NewParticipante(ParticipanteEnCuestDTO participante);
    Task UsuariosEnLaSala(List<string> usuarios);
    Task getUsers(int code);

}
