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
    [Route("api/Respuestas")]
    [ApiController]
    public class RespuestasController : ControllerBase
    {
        private readonly TestAnskusContext _context;
        private readonly IMapper _mapper;
        public RespuestasController(TestAnskusContext context, IMapper mapper)
        {
            _mapper= mapper;
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
        public async Task<IActionResult> PutRespuesta(List<RespuestasDTO> respuestasDTO)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
      
            }
            catch (Exception ex)
            {
                responseAPI.mensaje = "Ocurrio un error al editar la pregunta de tipo: " + ex.Message;
                responseAPI.EsCorrecto = false;
            }
            return Ok(responseAPI);
        }

        // POST: api/Respuestas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateRespuestas( List<RespuestasDTO> respuestaDTO)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
                var respuestaS = _mapper.Map<Respuesta>(respuestaDTO);
                _context.Respuestas.AddRange(respuestaS);
                await _context.SaveChangesAsync();
                

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
