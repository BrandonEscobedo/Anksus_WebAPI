namespace TestAnskus.Client.Services.Interfaces
{
    public interface IIdContainer
    {
        public int idCuestionario { get; set; }
        public int idPregunta { get; set; }
        public void SetIdCuestionario(int id);
        public void SetIdPregunta(int id);
    }
}
