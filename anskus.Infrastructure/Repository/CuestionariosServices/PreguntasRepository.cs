using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Services;
using anskus.Domain.Cuestionarios;
using anskus.Domain.Models.dbModels;
using anskus.Infrastructure.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrastructure.Repository.CuestionariosServices
{
    internal sealed class PreguntasRepository : IPreguntasRepository
    {
        private readonly TestAnskusContext _context;

        public PreguntasRepository(TestAnskusContext testAnskusContext)
        {
            _context = testAnskusContext;

        }

        public async Task<int> Add(Pregunta pregunta)
        {
            try
            {
                if (pregunta != null)
                {
                     _context.Preguntas.Add(pregunta);
                    await _context.SaveChangesAsync();
                    return pregunta.IdPregunta;
                }
                return 0;
             
            }
            catch 
            {
                return 0;
            }

        }
       public async Task<bool> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    var preg = _context.Preguntas.Where(x => x.IdPregunta == id).FirstOrDefault();
                    if (preg != null)
                    {
                        _context.Preguntas.Remove(preg);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
