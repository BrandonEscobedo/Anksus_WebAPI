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
        public int IdPeC { get; set; }   
        public string Nombre { get; set; } = null!;
        public int? Puntos { get; set; }
        public string codigo { get; set; } =string.Empty;
        public int? CantidadPacertadas { get; set; }
    }
}
