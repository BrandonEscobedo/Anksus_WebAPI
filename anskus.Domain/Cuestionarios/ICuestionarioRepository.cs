using anskus.Domain.Models.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace anskus.Domain.Cuestionarios
{
    public interface ICuestionarioRepository
    {
          Task<int> Add(Cuestionario cuestionario,string email);
          Task<List<Cuestionario>> GetbyUser(string email);
          Task<bool> Update(Cuestionario cuestionario );
    }
}
