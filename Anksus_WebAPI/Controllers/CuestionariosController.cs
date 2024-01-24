using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anksus_WebAPI.Models.dbModels;
using Microsoft.AspNetCore.Identity;
using Anksus_WebAPI.Models.DTO;
using TestAnskus.Shared;
using System.Security.Claims;

namespace Anksus_WebAPI.Controllers
{
    [Route("api/Cuestionarios")]
    [ApiController]
    public class CuestionariosController : ControllerBase
    {
        private readonly TestAnskusContext _context;
        private readonly UserManager<AplicationUser> _userManager;
        public CuestionariosController(TestAnskusContext context,UserManager<AplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/Cuestionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuestionario>>> GetCuestionarios()
        {
            return await _context.Cuestionarios.ToListAsync();
        }

        // GET: api/Cuestionarios/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Cuestionario>> GetCuestionario(int id)
        //{

        //}

        // PUT: api/Cuestionarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCuestionario(int id, CuestionarioDTO cuestionarioDTO)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
                var Cuestionario = await _context.Cuestionarios.FirstOrDefaultAsync(e => e.IdCuestionario==id);
                if (Cuestionario != null) 
                {
                    Cuestionario.Titulo=cuestionarioDTO.Titulo;
                    Cuestionario.IdCategoria = cuestionarioDTO.IdCategoria;
                    Cuestionario.Publico = cuestionarioDTO.Publico;
                    _context.Update(Cuestionario);
                    await _context.SaveChangesAsync();
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = Cuestionario.IdCuestionario;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.mensaje = "Cuestionario no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.mensaje = "Error al actualizar de tipo: "+ex.Message;
            }

            return Ok(responseAPI);
        }

        // POST: api/Cuestionarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateCuestionario(CuestionarioDTO cuestionario)
        {
        
            var responseAPI = new ResponseAPI<int>();
            try
            {
                var email = User.Identity?.Name;
                var user = await _userManager.FindByEmailAsync(email!);
                var user2 = await _userManager.GetUserAsync(User);
                Cuestionario cuest = new Cuestionario()
                {
                    IdCategoria = cuestionario.IdCategoria,
                    IdUsuario = user!.Id,
                    Estado = cuestionario.Estado,
                    Publico = cuestionario.Publico,
                    Titulo = cuestionario.Titulo

                };

                _context.Cuestionarios.Add(cuest);
                await _context.SaveChangesAsync();
                if (cuest.IdCuestionario != 0)
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = cuest.IdCuestionario;
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
                responseAPI.mensaje = ex.Message;
            }
            return Ok(responseAPI);
          
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
    }
}
