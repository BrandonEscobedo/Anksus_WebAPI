using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAnskus.Shared
{
    public class ParticipanteEnCuestDTO
    {    
        public Guid IdPeC { get; set; }   
        public string Nombre { get; set; } = null!;
        public int? Puntos { get; set; }
        public int codigo { get; set; } 
        public int? CantidadPacertadas { get; set; }

        public ParticipanteEnCuestDTO()
        {
            IdPeC = Guid.NewGuid();
        }
    }
}
