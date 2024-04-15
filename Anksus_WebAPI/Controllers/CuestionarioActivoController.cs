using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anksus_WebAPI.Models.dbModels;
using Microsoft.AspNetCore.Identity;
using anskus.Application.DTOs.Cuestionarios;
using Anksus_WebAPI.Server.Servicios;
using anskus.Application.Cuestionarios;

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/CuestionarioActivo")]
    [ApiController]
    public class CuestionarioActivoController : ControllerBase
    {
        private readonly TestAnskusContext _context;
        public CuestionarioActivoController(TestAnskusContext context )
        {
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
           
            //var responseAPI = new ResponseAPI<object>();
            //try
            //{
            //    RandomCodeService codigoRandom = new RandomCodeService(_context);
            //    var cuestionario = await _context.Cuestionarios.FindAsync(idcuestionario);
            //    var user = await _userManager.FindByEmailAsync(User.Identity!.Name!) ?? throw new Exception("Usuario no Encontrado");
            //    if (cuestionario!=null && cuestionario.Estado==false)
            //    {
            //      var CuestionarioActivo=  await _context.CuestionarioActivos.FirstOrDefaultAsync(e => e.IdCuestionario == idcuestionario
            //        && e.IdUsuario == user!.Id);
            //        CuestionarioActivo cuestionarioA = new CuestionarioActivo()
            //        {
            //            IdCuestionario = idcuestionario,
            //            IdUsuario = user!.Id,
            //            Codigo = 0
            //        };

            //        if (CuestionarioActivo == null)              
            //        {
            //            cuestionarioA.Codigo = await codigoRandom.GenerarCodigo();
            //            _context.CuestionarioActivos.Add(cuestionarioA);
            //           await _context.SaveChangesAsync();
            //            responseAPI.EsCorrecto = true;
            //            CuestionarioActivoDTO cuestionarioActivoDTO = new CuestionarioActivoDTO();
            //            cuestionarioActivoDTO.idcuestionario = cuestionarioA.IdCuestionario;
            //            cuestionarioActivoDTO.codigo = cuestionarioA.Codigo;
            //            responseAPI.Valor = cuestionarioActivoDTO;
            //        }
            //        else
            //        {
            //            responseAPI.mensaje = "Ya tienes Un Cuestionario Activo";
            //            responseAPI.EsCorrecto = false;
            //            responseAPI.Valor = CuestionarioActivo.Codigo;
            //        }
            //    }
            //    return Ok(responseAPI);
            //}
            //catch(Exception ex)
            //{
            //    responseAPI.EsCorrecto = false;
            //    throw new Exception ("Se ha generado una exepcion al activar este cuestionario." +ex.Message);
            //}
            return Ok();
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
