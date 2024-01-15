using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anksus_WebAPI.Models.dbModels;

namespace Anksus_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuestionariosController : ControllerBase
    {
        private readonly TestAnskusContext _context;

        public CuestionariosController(TestAnskusContext context)
        {
            _context = context;
        }

        // GET: api/Cuestionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuestionario>>> GetCuestionarios()
        {
            return await _context.Cuestionarios.ToListAsync();
        }

        // GET: api/Cuestionarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuestionario>> GetCuestionario(int id)
        {
            var cuestionario = await _context.Cuestionarios.FindAsync(id);

            if (cuestionario == null)
            {
                return NotFound();
            }

            return cuestionario;
        }

        // PUT: api/Cuestionarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuestionario(int id, Cuestionario cuestionario)
        {
            if (id != cuestionario.IdCuestionario)
            {
                return BadRequest();
            }

            _context.Entry(cuestionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuestionarioExists(id))
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

        // POST: api/Cuestionarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuestionario>> PostCuestionario(Cuestionario cuestionario)
        {
            _context.Cuestionarios.Add(cuestionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuestionario", new { id = cuestionario.IdCuestionario }, cuestionario);
        }

        // DELETE: api/Cuestionarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuestionario(int id)
        {
            var cuestionario = await _context.Cuestionarios.FindAsync(id);
            if (cuestionario == null)
            {
                return NotFound();
            }

            _context.Cuestionarios.Remove(cuestionario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuestionarioExists(int id)
        {
            return _context.Cuestionarios.Any(e => e.IdCuestionario == id);
        }
    }
}
