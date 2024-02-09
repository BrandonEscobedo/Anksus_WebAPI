using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anksus_WebAPI.Models.dbModels;
using Microsoft.AspNetCore.Identity;
using TestAnskus.Shared;
using Anksus_WebAPI.Models.DTO;
using Anksus_WebAPI.Server.Servicios;
using Anksus_WebAPI.Server.Hubs.Implementacion;
using Microsoft.AspNetCore.SignalR;
using Anksus_WebAPI.Server.Hubs;

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/CuestionarioActivo")]
    [ApiController]
    public class CuestionarioActivoController : ControllerBase
    {
        private readonly TestAnskusContext _context;
        private readonly UserManager<AplicationUser> _userManager;

        public CuestionarioActivoController(TestAnskusContext context, UserManager<AplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            
          
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CuestionarioActivo>> GetCuestionarioActivo(int id)
        {
            var cuestionarioActivo = await _context.CuestionarioActivos.FindAsync(id);

            if (cuestionarioActivo == null)
            {
                return NotFound();
            }

            return cuestionarioActivo;
        }
    
 

    [HttpPost]
        public async Task<IActionResult> CreateCuestionarioActivo(int idcuestionario)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
                RandomCodeService codigoRandom = new RandomCodeService(_context);
                var cuestionario = await _context.Cuestionarios.FindAsync(idcuestionario);
                var user = await _userManager.FindByEmailAsync(User.Identity!.Name!) ?? throw new Exception("Usuario no Encontrado");
                if (cuestionario!=null && cuestionario.Estado==false)
                {
                  var CuestionarioActivo=  await _context.CuestionarioActivos.FirstOrDefaultAsync(e => e.IdCuestionario == idcuestionario
                    && e.IdUsuario == user!.Id);
                    CuestionarioActivo cuestionarioA = new CuestionarioActivo()
                    {
                        IdCuestionario = idcuestionario,
                        IdUsuario = user!.Id,
                        Codigo = 0
                    };

                    if (CuestionarioActivo == null)              
                    {
                        cuestionarioA.Codigo = await codigoRandom.GenerarCodigo();
                        _context.CuestionarioActivos.Add(cuestionarioA);
                       await _context.SaveChangesAsync();
                        responseAPI.EsCorrecto = true;                       
                        responseAPI.Valor = cuestionarioA.Codigo;
                    }
                    else
                    {
                        responseAPI.mensaje = "Ya tienes Un Cuestionario Activo";
                        responseAPI.EsCorrecto = false;
                        responseAPI.Valor = cuestionarioA.Codigo;
                    }
                }
                return Ok(responseAPI);
            }
            catch(Exception ex)
            {
                responseAPI.EsCorrecto = false;
                throw new Exception ("Se ha generado una exepcion al activar este cuestionario." +ex.Message);
            }
        }

    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuestionarioActivo(int id)
        {
            var cuestionarioActivo = await _context.CuestionarioActivos.FindAsync(id);
            if (cuestionarioActivo == null)
            {
                return NotFound();
            }

            _context.CuestionarioActivos.Remove(cuestionarioActivo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
