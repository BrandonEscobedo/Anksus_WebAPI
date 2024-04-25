using anskus.Application.DTOs.Cuestionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.HubSignalr.StateContainerHub
{
    public class HubStateCreador : IHubStateCreador
    {
        public List<ParticipanteEnCuestDTO> ListaPuntos { get; set; } = new();
        public event Action<List<ParticipanteEnCuestDTO>>? OnListaPuntos;

        public void AddParticipantePuntos(ParticipanteEnCuestDTO participante)
        {
            ListaPuntos.Add(participante);
        }

    }
}
