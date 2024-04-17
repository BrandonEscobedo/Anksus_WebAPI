using Anksus_WebAPI.Models.dbModels;
using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Services;
using anskus.Domain.Cuestionarios;
using AutoMapper;
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
    
        public PreguntasRepository(TestAnskusContext testAnskusContext  )
        {
            _context = testAnskusContext;
         
        }

        public async Task Add(Pregunta pregunta)
        {
            //try
            //{
            //    if (pregunta != null)
            //    {
            //        Pregunta preg = new Pregunta()
            //        {
            //            Pregunta1 = pregunta.Pregunta,
            //            IdCuestionario = pregunta.IdCuestionario,
            //            Estado = false,
            //        };
            //        var preguntaD = _context.Preguntas.Add(preg);
            //         var preg1=    mapper.Map<PreguntasDTO>(preg);
            //        await _context.SaveChangesAsync();
                              
            //        return new PreguntasResponse(pregunta, true, "Todo correcto");
            //    }
            //    return new PreguntasResponse(pregunta, false, "Error");
            //}
            //catch (Exception ex)
            //{
            //    return new PreguntasResponse(pregunta, false, ex.Message);
            //}
            
        }

        public Task GetPreguntaById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
