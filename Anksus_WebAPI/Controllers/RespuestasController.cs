using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using anskus.Application.DTOs.Cuestionarios;
using AutoMapper;
using MediatR;
using anskus.Application.Respuestas.Create;
using anskus.Domain.Models.dbModels;
using anskus.Infrastructure.Data;

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/Respuestas")]
    [ApiController]
    public class RespuestasController : ControllerBase
    {
        private readonly TestAnskusContext _context;
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        public RespuestasController(TestAnskusContext context, IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _context = context;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Respuesta>>> GetRespuestas()
        {
            return await _context.Respuestas.ToListAsync();
        }
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

     
        [HttpPut]
        public async Task<IActionResult> PutRespuesta(List<RespuestasDTO> respuestasDTO)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
                var ListaRespuestas= _mapper.Map<List<Respuesta>>(respuestasDTO);
                _context.Respuestas.UpdateRange(ListaRespuestas);
                await _context.SaveChangesAsync();
                responseAPI.EsCorrecto = true;
            }
            catch (DbUpdateException)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.mensaje = "Error al actualizar los datos";
            }
            catch (Exception Ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.mensaje = "No guardado " + Ex.Message;
            }
            return Ok(responseAPI);
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateRespuestas( List<RespuestasDTO> respuestaDTO)
        {

        var response= await _mediator.Send( new CreateRespuestasCommand(respuestaDTO));
            return Ok(response);

         
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
