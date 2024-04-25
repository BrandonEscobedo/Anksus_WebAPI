namespace anskus.Application.DTOs.Cuestionarios
{
    public class ParticipanteEnCuestDTO
    {    
        public Guid IdPeC { get; set; }   
        public string Nombre { get; set; } = null!;
        public int? PuntosActuales { get; set; }
        public int? PuntosAnteriores { get; set; }
        public int codigo { get; set; } 
        public int? CantidadPacertadas { get; set; }

        public ParticipanteEnCuestDTO()
        {
            IdPeC = Guid.NewGuid();
        }


        //metodos para manipular Participante
    }
}
