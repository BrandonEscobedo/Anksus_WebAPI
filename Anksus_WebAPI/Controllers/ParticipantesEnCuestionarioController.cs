using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anksus_WebAPI.Models.dbModels;
using TestAnskus.Shared;

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/ParticipantesEnCuestionario")]
    [ApiController]
    public class ParticipantesEnCuestionarioController : ControllerBase
    {
        private readonly TestAnskusContext _context;

        public ParticipantesEnCuestionarioController(TestAnskusContext context)
        {
            _context = context;
        }

        // GET: api/ParticipantesEnCuestionario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipanteEnCuestionario>>> GetParticipanteEnCuestionarios()
        {
            return await _context.ParticipanteEnCuestionarios.ToListAsync();
        }

        // GET: api/ParticipantesEnCuestionario/5
        [HttpGet("{code}")]
        public async Task<bool> IsCodeValid(int code)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
              
                if (_context.CuestionarioActivos.Where(c => c.Codigo == code).Any()==false) responseAPI.EsCorrecto = false;
                else
                    responseAPI.EsCorrecto = true;
            }

            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.mensaje = "Ocurrio un error al verificar el codigo de tipo: " + ex.Message;
            }
            return responseAPI.EsCorrecto;
        }
        // PUT: api/ParticipantesEnCuestionario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipanteEnCuestionario(int id, ParticipanteEnCuestionario participanteEnCuestionario)
        {
            if (id != participanteEnCuestionario.IdPeC)
            {
                return BadRequest();
            }

            _context.Entry(participanteEnCuestionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteEnCuestionarioExists(id))
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

        // POST: api/ParticipantesEnCuestionario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParticipanteEnCuestionario>> PostParticipanteEnCuestionario(ParticipanteEnCuestionario participanteEnCuestionario)
        {
            _context.ParticipanteEnCuestionarios.Add(participanteEnCuestionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipanteEnCuestionario", new { id = participanteEnCuestionario.IdPeC }, participanteEnCuestionario);
        }

        // DELETE: api/ParticipantesEnCuestionario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipanteEnCuestionario(int id)
        {
            var participanteEnCuestionario = await _context.ParticipanteEnCuestionarios.FindAsync(id);
            if (participanteEnCuestionario == null)
            {
                return NotFound();
            }

            _context.ParticipanteEnCuestionarios.Remove(participanteEnCuestionario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipanteEnCuestionarioExists(int id)
        {
            return _context.ParticipanteEnCuestionarios.Any(e => e.IdPeC == id);
        }
    }
}
