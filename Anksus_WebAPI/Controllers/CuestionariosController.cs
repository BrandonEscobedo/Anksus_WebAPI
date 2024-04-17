using Microsoft.AspNetCore.Mvc;
using Anksus_WebAPI.Models.dbModels;
using anskus.Application.DTOs.Cuestionarios;
using AutoMapper;
using anskus.Application.Services;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Cuestionarios;
using MediatR;
using anskus.Application.Cuestionarios.Create;
using System.Security.Claims;
namespace Anksus_WebAPI.Controllers
{
    [Route("api/Cuestionarios")]
    [ApiController]
    public class CuestionariosController(ISender _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCuestionario(CreateCuestionarioCommand cuestionario)
        {
            var useri2d=User.FindFirst(ClaimTypes.Name)?.Value;
            var result =await _mediator.Send(cuestionario);
            return Ok(result);

            //var user = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
         
          //  if (cuestionario.Email == null)
          //  {
          //      return Unauthorized();
          //  }
          //var cuestionarioId=  await cuestionarioRepository.Add(cuestionario);
          //  return Ok(new CuestionarioResponse(cuestionarioId.idcuestionario,"",true));
        }
        // PUT: api/Cuestionarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> EditCuestionario(CuestionarioDTO cuestionarioDTO)
        {
            //if (cuestionarioDTO.IdCuestionario == 0)
            //    return StatusCode(503);
            //var cuest = await cuestionarioRepository.Update( cuestionarioDTO);
            //return Ok(new CuestionarioResponse(cuest.idcuestionario, "", true));
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cuestionario>> GetCuestionarios(int id)
        {
        //    var responseAPI = new ResponseAPI<CuestionarioDTO>();
        //    try
        //    {
        //        var cuestionarios = _mapper.Map<CuestionarioDTO>(await _context.Cuestionarios
        //            .Where(e => e.IdCuestionario == id)
        //            .Include(o => o.Pregunta)
        //            .ThenInclude(x=>x.Respuesta)
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

           return Ok();
        }

        // GET: api/Cuestionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuestionario>>> GetCuestionarios()
        {
            //var responseAPI = new ResponseAPI<List<CuestionarioDTO>>();
            //try
            //{
            //    var email = User.Identity?.Name;
            //    var cuestionarios = _mapper.Map<List<CuestionarioDTO>>(await _context.Cuestionarios
            //        .Where(e => e.IdUsuario == user!.Id)
            //        .Include(o=>o.Pregunta).ToListAsync());
            //    if (cuestionarios != null)
            //    {
            //        responseAPI.Valor = cuestionarios;
            //        responseAPI.EsCorrecto = true;
            //    }
            //    else
            //    {
            //        responseAPI.mensaje = "Este usuario no tiene Cuestionarios Aun.";
            //        responseAPI.EsCorrecto = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    responseAPI.mensaje = "Ha ocurrido un error de tipo: " + ex.Message;
            //    responseAPI.EsCorrecto = false;
            //}

            return Ok();
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
