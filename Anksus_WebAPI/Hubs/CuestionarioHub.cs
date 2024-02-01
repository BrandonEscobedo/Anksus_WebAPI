using Anksus_WebAPI.Models.dbModels;
using Microsoft.AspNetCore.SignalR;
using TestAnskus.Shared;

namespace Anksus_WebAPI.Server.Hubs
{
    public class CuestionarioHub:Hub
    {
        public async Task JoinCuestionario(ParticipanteEnCuestDTO participante)
        {
            await Clients.All.SendAsync("ReciveMessage","admin", $"{participante.Nombre} se ha unido");
        }
        public async Task JoinSpecificCuestionario(ParticipanteEnCuestDTO participante)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, participante.codigo);
            await Clients.Group(participante.codigo).SendAsync("ReciveMessage", "admin", $"{participante.Nombre} ha entrado {participante.codigo}");

        }
    }
}
