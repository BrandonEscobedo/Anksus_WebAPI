namespace anskus.Application.DTOs.Cuestionarios
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
