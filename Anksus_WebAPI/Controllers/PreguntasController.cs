using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;
using TestAnskus.Shared;
using AutoMapper;

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/Preguntas")]
    [ApiController]
    public class PreguntasController : ControllerBase
    {
        private readonly TestAnskusContext _context;
        private readonly IMapper _mapper;
        public PreguntasController(TestAnskusContext context,IMapper mapper)
        {
            _mapper=mapper;
            _context = context;
        }

        // GET: api/Preguntas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pregunta>>> GetPreguntas()
        {
            return await _context.Preguntas.ToListAsync();
        }

        // GET: api/Preguntas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPregunta(int id)
        {
            var responseAPi = new ResponseAPI<PreguntasDTO>();
            try
            {
                
                var pregunta = await _context.Preguntas.FindAsync(id);
                if (pregunta != null)
                {
                    var preguntaDTO = _mapper.Map<PreguntasDTO>(pregunta);
                    responseAPi.EsCorrecto = true;
                    responseAPi.Valor = preguntaDTO;
                }
                else
                {
                    responseAPi.EsCorrecto = false;
                    responseAPi.mensaje = "Error al obtener pregunta";
                }
                
            }
            catch (Exception ex)
            {
                responseAPi.EsCorrecto = false;
                responseAPi.mensaje = "Error al obtener pregunta generado por: "+ex.Message;
            }
            return Ok(responseAPi);
        }

        // PUT: api/Preguntas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPregunta(int id, Pregunta pregunta)
        {
            if (id != pregunta.IdPregunta)
            {
                return BadRequest();
            }

            _context.Entry(pregunta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        // POST: api/Preguntas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<IActionResult>  CreatePreguntas(PreguntasDTO _pregunta)
            {
            var responseAPI = new ResponseAPI<int>();
            try
            {
            Pregunta pregunta = new Pregunta()
            {
            IdCuestionario = _pregunta.IdCuestionario,
            Pregunta1 = _pregunta.Pregunta,
            Estado = false

            };
            _context.Preguntas.Add(pregunta);
            await _context.SaveChangesAsync();
            if (pregunta.IdPregunta != 0)
            {
            responseAPI.EsCorrecto = true;
            responseAPI.Valor = pregunta.IdPregunta;
            }
            else
            {
            responseAPI.EsCorrecto = true;
            responseAPI.mensaje = "No guardado";
            }
            }
            catch(Exception ex)
            {
            responseAPI.EsCorrecto = false;
            responseAPI.mensaje = "No guardado "  +ex.Message;

            }
            return Ok(responseAPI);

        }

            // DELETE: api/Preguntas/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePregunta(int id)
            {
                var responseAPI = new ResponseAPI<int>();
            try
            {
                var pregunta = await _context.Preguntas.FindAsync(id);
                if(pregunta != null )
                {
                    _context.Remove(pregunta);
                    await _context.SaveChangesAsync();
                    responseAPI.EsCorrecto = true;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.mensaje = "Pregunta no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.mensaje = "Error generado por "  +ex.Message;
            }
            return Ok(responseAPI);  
            }
        }
}
