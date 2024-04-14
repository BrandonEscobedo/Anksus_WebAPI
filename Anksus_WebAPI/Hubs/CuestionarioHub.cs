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
        public async Task<bool> CreateRoom(int code, List<PreguntasDTO> preguntas, string titulo)
        {
            await _distributedCache.CreateRoomCache(code.ToString());
            Context.Items["Preguntas"] =preguntas;
            Context.Items["Titulo"] =titulo;
            Context.Items["Codigo"] = code;
            await Groups.AddToGroupAsync(Context.ConnectionId, code.ToString());
            return true;
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
        public async Task SiguientePregunta()
        {
            var preguntas = (List<PreguntasDTO?>?)Context.Items["Preguntas"];
            if (preguntas != null && preguntas.Count > 0)
            {
                int? Room =(int?)Context.Items["Codigo"];
                if (Room != null && Room!=0)
                {
                    string? titulo = (string?)Context.Items["Titulo"];
                    await Clients.Group(Room.ToString()!).SiguientePregunta(preguntas.First()!,titulo);
                    preguntas.RemoveAt(0);
                    Context.Items["Preguntas"] = preguntas;
                }
               
            }

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            int? code = (int?)Context.Items["Codigo"];
            if (code != null && code != 0)
            {
                //var cuestionario = _context.CuestionarioActivos.Where(x => x.Codigo == code).FirstOrDefault();
                //if (cuestionario != null)
                //{
                //    _context.CuestionarioActivos.Remove(cuestionario);
                //    await _context.SaveChangesAsync();
                //}
            }
            else
            {
                ParticipanteEnCuestDTO? participante = (ParticipanteEnCuestDTO?)Context.Items["UserData"];
                if (participante != null)
                {
                    await _distributedCache.RemoveUserFromRoom(participante.codigo.ToString(), participante);
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, participante.codigo.ToString());
                    await Clients.Group(participante.codigo.ToString()).RemoveUser(participante);
                }
            }
            Context.Items.Clear();
        }


    }
}
public interface InotificationClient
{
    Task IniciarCuestionario(List<PreguntasDTO> cuestionarios);
    Task SiguientePregunta(PreguntasDTO pregunta, string? titulo);
    Task MensajePrueba(string mensaje);
    Task RemoveUser(ParticipanteEnCuestDTO participante);
    Task NewParticipante(ParticipanteEnCuestDTO participante);
    Task UsuariosEnLaSala(List<string> usuarios);
    Task getUsers(int code);

}
