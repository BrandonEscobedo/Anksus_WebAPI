﻿using Anksus_WebAPI.Controllers;
using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;
using Anksus_WebAPI.Server.Hubs.Implementacion;
using Anksus_WebAPI.Server.Servicios;
using Microsoft.AspNetCore.SignalR;
using TestAnskus.Shared;

namespace Anksus_WebAPI.Server.Hubs
{
    public class CuestionarioHub:Hub<InotificationClient>
    {
        static int count = 0;
        static int code = 0;
        private readonly TestAnskusContext _context;
        private static   List<ParticipanteEnCuestDTO> Participantes = new List<ParticipanteEnCuestDTO>();
        public CuestionarioHub(TestAnskusContext context  )
        {
            _context = context;
        }
        public override async Task OnConnectedAsync()
        {
            count++;
            await Clients.All.UpdatesClientsCount(count);
            await Clients.All.UsuariosEnLaSala(Participantes.Select(p => p.Nombre).ToList());
            await base.OnConnectedAsync();
            
        }     
      
      public  Task JoinRoom(int code)
        {
           
          return Groups.AddToGroupAsync(Context.ConnectionId, code.ToString());
            
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            count--;
            await Clients.All.UpdatesClientsCount(count);
            await  base.OnDisconnectedAsync(exception);
        }
        public async Task IngresarUsuario(string nombre, int codigo)
        {
            ParticipanteEnCuestDTO p = new ParticipanteEnCuestDTO
            {

                codigo = codigo,
                Nombre = nombre
            };
            Participantes.Add(p);
            await Groups.AddToGroupAsync(Context.ConnectionId, codigo.ToString());
            await Clients.Group(codigo.ToString()).receiveParticipante(p);
            await Clients.Group(codigo.ToString()).UsuariosEnLaSala(GetParticipantesName());
        }
        private List<string> GetParticipantesName()
        {
            return Participantes.Select(x => x.Nombre).ToList();
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
