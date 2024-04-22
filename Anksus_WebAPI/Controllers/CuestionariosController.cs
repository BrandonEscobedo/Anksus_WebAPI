using Microsoft.AspNetCore.Mvc;
using anskus.Application.DTOs.Cuestionarios;
using MediatR;
using System.Security.Claims;
using anskus.Application.Cuestionarios.Commands.Create;
using anskus.Application.Cuestionarios.Commands.Update;
using anskus.Application.Cuestionarios.Querys.GetById;
using Microsoft.AspNetCore.Authorization;
namespace Anksus_WebAPI.Controllers
{
    [Authorize]
    [Route("api/Cuestionarios")]
    [ApiController]
    public class CuestionariosController(ISender _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCuestionario(CuestionarioDTO cuestionario)
        {     
            if (User.Identity?.IsAuthenticated==true)
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                if (userEmail != null)
                {
                    var result = await _mediator.Send(new CreateCuestionarioCommand(cuestionario, userEmail));
                    return Ok(result);
                }         
            }
            return StatusCode(503);       
        }
        // PUT: api/Cuestionarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> EditCuestionario(CuestionarioDTO cuestionarioDTO)
        {
            if(cuestionarioDTO!=null && User.Identity?.IsAuthenticated == true)
            {
                var result = await _mediator.Send(new UpdateCuestionarioCommand(cuestionarioDTO));
                return Ok(result);
            }
            return StatusCode(503);
        }

        //[")]
        //public async Task<ActionResult<Cuestionario>> GetCuestionarios(int id)
        //{
        //    var responseAPI = new ResponseAPI<CuestionarioDTO>();
        //    try
        //    {
        //        var cuestionarios = _mapper.Map<CuestionarioDTO>(await _context.Cuestionarios
        //            .Where(e => e.IdCuestionario == id)
        //            .Include(o => o.Pregunta)
        //            .ThenInclude(x => x.Respuesta)
        //            .FirstOrDefaultAsync());
        //        if (cuestionarios != null)
        //        {
        //            return Ok(cuestionarios);
        //        }
        //        else
        //        {
        //            responseAPI.mensaje = "Este usuario no tiene Cuestionarios Aun.";
        //            responseAPI.EsCorrecto = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        responseAPI.mensaje = "Ha ocurrido un error de tipo: " + ex.Message;
        //        responseAPI.EsCorrecto = false;
        //    }

        //    return Ok();
        //}

        // GET: api/Cuestionarios
        [HttpGet("{email}")]
        public async Task<ActionResult<List<CuestionarioDTO>>> GetCuestionarios(string email)
        {
            var result = await _mediator.Send(new GetCuestionarioByUserQuery(email));
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

      
        // DELETE: api/Cuestionarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuestionario(int id)
        {
        //    var responseAPI = new ResponseAPI<int>();
        //    try
        //    {
        //var cuestionario = await _context.Cuestionarios.FindAsync(id);
        //            if (cuestionario != null)
        //            {
        //           _context.Cuestionarios.Remove(cuestionario);
        //            await _context.SaveChangesAsync();
        //            }

                   
        //    }
        //    catch(Exception ex)
        //    {
        //        responseAPI.EsCorrecto = false;
        //        responseAPI.mensaje="Hubo un error al eliminar el cuestionario de tipo: " +ex.Message;
        //    }
            

            return NoContent();
        }
    }
}
