using Anksus_WebAPI.Models.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace anskus.Domain.Cuestionarios
{
    public interface ICuestionarioRepository
    {
          Task<int> Add(Cuestionario cuestionario);
    }
}
