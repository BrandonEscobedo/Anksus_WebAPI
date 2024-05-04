using anskus.Domain.Models.dbModels;
namespace anskus.Domain.Cuestionarios
{
    public interface ICuestionarioRepository
    {
          Task<int> Add(Cuestionario cuestionario,string email);
          Task<List<Cuestionario>> GetbyUser(string email);
          Task<Cuestionario> GetbyId(int id);
          Task<bool> Update(Cuestionario cuestionario);
    }
}
