using anskus.Application.Cuestionarios;
namespace anskus.Application.Services
{
    public class IdContainer:IIdContainer
    {
        public int idCuestionario { get; set; } = 0;
        public int idPregunta { get; set; } = 0;
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
