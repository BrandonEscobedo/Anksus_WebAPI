using TestAnskus.Client.Services.Interfaces;

namespace TestAnskus.Client.Services.Implementacion
{
    public class IdConteiner : IIdContainer
    {
        public int idCuestionario { get; set; }
        public int idPregunta { get;set; }
        public void SetIdCuestionario(int id)
        {
            idCuestionario = id;
        } 
        public void SetIdPregunta(int id)
        {
            idPregunta = id;
        }
    }
}
