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

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/Respuestas")]
    [ApiController]
    public class RespuestasController : ControllerBase
    {
        private readonly TestAnskusContext _context;

        public RespuestasController(TestAnskusContext context)
        {
            _context = context;
        }

        // GET: api/Respuestas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Respuesta>>> GetRespuestas()
        {
            return await _context.Respuestas.ToListAsync();
        }

        // GET: api/Respuestas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Respuesta>> GetRespuesta(int id)
        {
            var respuesta = await _context.Respuestas.FindAsync(id);

            if (respuesta == null)
            {
                return NotFound();
            }

            return respuesta;
        }

        // PUT: api/Respuestas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRespuesta(int id, Respuesta respuesta)
        {
            if (id != respuesta.IdRespuesta)
            {
                return BadRequest();
            }

            _context.Entry(respuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RespuestaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Respuestas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateRespuestas(List<RespuestasDTO> respuestaDTO)
        {
            var responseAPI = new ResponseAPI<int>();

            int id =0;
            try
            {
                foreach(var respuesta in respuestaDTO)
                {
                    var nuevaRespuesta = new Respuesta
                    {
                        Respuesta1=respuesta.respuesta,
                        RCorrecta=respuesta.RCorrecta,
                        IdPregunta=respuesta.IdPregunta
                    };
                    _context.Respuestas.AddRange(nuevaRespuesta);
                    await _context.SaveChangesAsync();
                    id = nuevaRespuesta.IdRespuesta;
                }
                if (id != 0)
                {
                    responseAPI.EsCorrecto = true;
                }
                else
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.mensaje = "No guardado";
                }

            }
            catch(Exception Ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.mensaje = "No guardado " + Ex.Message;
            }
            return Ok(responseAPI);
            

         
        }

        // DELETE: api/Respuestas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRespuesta(int id)
        {
            var respuesta = await _context.Respuestas.FindAsync(id);
            if (respuesta == null)
            {
                return NotFound();
            }

            _context.Respuestas.Remove(respuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RespuestaExists(int id)
        {
            return _context.Respuestas.Any(e => e.IdRespuesta == id);
        }
    }
}
