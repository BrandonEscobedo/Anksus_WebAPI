using Anksus_WebAPI.Models.dbModels;
using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Services
{
    public interface ICuestionarioRepository
    {
         Task<GeneralResponse> Add(CuestionarioDTO cuestionario, string id);
        void Update(Cuestionario cuestionario);
        void Delete(int id);
        Task<Cuestionario> GeyByIdAsync(int id);
        Task<IEnumerable<Cuestionario>> GetAllAsync();

    }
}
