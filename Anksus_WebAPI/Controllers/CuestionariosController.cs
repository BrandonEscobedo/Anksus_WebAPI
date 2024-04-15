﻿using Microsoft.AspNetCore.Mvc;
using Anksus_WebAPI.Models.dbModels;
using anskus.Application.DTOs.Cuestionarios;
using AutoMapper;
using anskus.Application.Services;
using anskus.Application.DTOs.Response;
namespace Anksus_WebAPI.Controllers
{
    [Route("api/Cuestionarios")]
    [ApiController]
    public class CuestionariosController(ICuestionarioRepository cuestionarioRepository, IMapper _mapper) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> CreateCuestionario(CuestionarioDTO cuestionario)
        {
            //var user = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
          
            if (cuestionario.Email == null)
            {
                return Unauthorized();
            }

            return Ok(await cuestionarioRepository.Add(cuestionario)) ;
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

        // PUT: api/Cuestionarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCuestionario(int id, CuestionarioDTO cuestionarioDTO)
        {
            //var responseAPI = new ResponseAPI<int>();
            //try
            //{
            //    var Cuestionario = await _context.Cuestionarios.FirstOrDefaultAsync(e => e.IdCuestionario==id);
            //    if (Cuestionario != null) 
            //    {
            //        Cuestionario.Titulo=cuestionarioDTO.Titulo;
            //        Cuestionario.IdCategoria = cuestionarioDTO.IdCategoria;
            //        Cuestionario.Publico = cuestionarioDTO.Publico;
            //        _context.Update(Cuestionario);
            //        await _context.SaveChangesAsync();
            //        responseAPI.EsCorrecto = true;
            //        responseAPI.Valor = Cuestionario.IdCuestionario;
            //    }
            //    else
            //    {
            //        responseAPI.EsCorrecto = false;
            //        responseAPI.mensaje = "Cuestionario no encontrado";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    responseAPI.EsCorrecto = false;
            //    responseAPI.mensaje = "Error al actualizar de tipo: "+ex.Message;
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
