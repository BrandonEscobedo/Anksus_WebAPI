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
using AutoMapper;

namespace Anksus_WebAPI.Controllers
{
    [Route("api/Cuestionarios")]
    [ApiController]
    public class CuestionariosController : ControllerBase
    {
        private readonly TestAnskusContext _context;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CuestionariosController(TestAnskusContext context,UserManager<AplicationUser> userManager,IMapper mapper)
        {
            _mapper=mapper;
            _userManager = userManager;
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuestionario>> GetCuestionarios(int id)
        {
            var responseAPI = new ResponseAPI<CuestionarioDTO>();
            try
            {          
                var cuestionarios = _mapper.Map<List<CuestionarioDTO>>(await _context.Cuestionarios
                    .Where(e => e.IdCuestionario == id)
                    .Include(o => o.Pregunta)
                    .ThenInclude(x=>x.Respuesta)                   
                    .ToListAsync());
                if (cuestionarios != null)
                {
                    return Ok(cuestionarios);
                }
                else
                {
                    responseAPI.mensaje = "Este usuario no tiene Cuestionarios Aun.";
                    responseAPI.EsCorrecto = false;
                }
            }
            catch (Exception ex)
            {
                responseAPI.mensaje = "Ha ocurrido un error de tipo: " + ex.Message;
                responseAPI.EsCorrecto = false;
            }

            return Ok(responseAPI);
        }

        // GET: api/Cuestionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuestionario>>> GetCuestionarios()
        {
            var responseAPI = new ResponseAPI<List<CuestionarioDTO>>();
            try
            {
                var email = User.Identity?.Name;
                var user = await _userManager.FindByEmailAsync(email!);
                var cuestionarios = _mapper.Map<List<CuestionarioDTO>>(await _context.Cuestionarios
                    .Where(e => e.IdUsuario == user!.Id)
                    .Include(o=>o.Pregunta).ToListAsync());
                if (cuestionarios != null)
                {
                    responseAPI.Valor = cuestionarios;
                    responseAPI.EsCorrecto = true;
                }
                else
                {
                    responseAPI.mensaje = "Este usuario no tiene Cuestionarios Aun.";
                    responseAPI.EsCorrecto = false;
                }
            }
            catch (Exception ex)
            {
                responseAPI.mensaje = "Ha ocurrido un error de tipo: " + ex.Message;
                responseAPI.EsCorrecto = false;
            }

            return Ok(responseAPI);
        }

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
            var responseAPI = new ResponseAPI<int>();
            try
            {
        var cuestionario = await _context.Cuestionarios.FindAsync(id);
                    if (cuestionario != null)
                    {
                   _context.Cuestionarios.Remove(cuestionario);
                    await _context.SaveChangesAsync();
                    }

                   
            }
            catch(Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.mensaje="Hubo un error al eliminar el cuestionario de tipo: " +ex.Message;
            }
            

            return NoContent();
        }
    }
}
