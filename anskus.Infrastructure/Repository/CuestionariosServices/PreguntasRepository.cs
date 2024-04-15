using Anksus_WebAPI.Models.dbModels;
using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrastructure.Repository.CuestionariosServices
{
    internal sealed class PreguntasRepository:IPreguntasRepository
    {
        private readonly TestAnskusContext _context;

        public PreguntasRepository(TestAnskusContext testAnskusContext)
        {
            _context = testAnskusContext;
        }

        public async Task<GeneralResponse> Add(PreguntasDTO pregunta)
        {

            Pregunta preg = new Pregunta()
            {
                Pregunta1 = pregunta.Pregunta,
                IdCuestionario=pregunta.IdCuestionario,
                Estado=false,                             
           };
            _context.Preguntas.Add(preg);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Todo correcto");


        }

        public Task GetPreguntaById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
