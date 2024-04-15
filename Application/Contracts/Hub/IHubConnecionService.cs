using anskus.Application.DTOs.Cuestionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Contracts.Hub
{
    public interface IHubConnecionService
    {
        public Task SiguientePreguntaUsuarios();
        public Task NewRom(int codigo, int idcuestionario);
        public Task AddUserToRoom(ParticipanteEnCuestDTO participante);

    }
}
