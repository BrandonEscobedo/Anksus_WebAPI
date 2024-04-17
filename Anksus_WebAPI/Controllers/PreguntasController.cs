using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anksus_WebAPI.Models.dbModels;
using anskus.Application.DTOs.Cuestionarios;
using AutoMapper;
using anskus.Application.Services;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Cuestionarios;

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/Preguntas")]
    [ApiController]
    public class PreguntasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPreguntasRepository _preguntasRepository;
        public PreguntasController(IMapper mapper, IPreguntasRepository preguntasRepository )
        {
            _mapper = mapper;
            _preguntasRepository = preguntasRepository;
        }

        // GET: api/Preguntas

        // GET: api/Preguntas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPregunta(int id)
        {
        //    var responseAPi = new ResponseAPI<PreguntasDTO>();
        //    try
        //    {
                
        //        var pregunta = await _context.Preguntas.FindAsync(id);
        //        if (pregunta != null)
        //        {
        //            var preguntaDTO = _mapper.Map<PreguntasDTO>(pregunta);
        //            responseAPi.EsCorrecto = true;
        //            responseAPi.Valor = preguntaDTO;
        //        }
        //        else
        //        {
        //            responseAPi.EsCorrecto = false;
        //            responseAPi.mensaje = "Error al obtener pregunta";
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        responseAPi.EsCorrecto = false;
        //        responseAPi.mensaje = "Error al obtener pregunta generado por: "+ex.Message;
        //    }
            return Ok();
        }

        // PUT: api/Preguntas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutPregunta( PreguntasDTO PreguntaDTO)
        {
            //var responseAPI = new ResponseAPI<int>();
            //try
            //{
            //    var pregunta = _context.Preguntas.Find(PreguntaDTO.IdPregunta);
            //    if(pregunta != null)
            //    {
            //        pregunta.Pregunta1 = PreguntaDTO.Pregunta;
            //        _context.Preguntas.Update(pregunta);
            //        await _context.SaveChangesAsync();
            //        responseAPI.EsCorrecto = true;
            //    }
            //    else { responseAPI.EsCorrecto = false; responseAPI.mensaje = "Ocurrio un error al encontrar la pregunta."; }
            //}
            //catch (Exception ex)
            //{
            //    responseAPI.mensaje = "error al editar pregunta en :" + ex.Message;
            //}

            return Ok();
        }

        // POST: api/Preguntas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task CreatePreguntas(Pregunta _pregunta)
        {

            //if (_pregunta == null)
            //    return StatusCode(503);
            //var preg = await _preguntasRepository.Add(_pregunta);
            //return Ok(new PreguntasResponse(_pregunta, true, ""));
        }
           
            //var responseAPI = new ResponseAPI<int>();
            //try
            //{
            //Pregunta pregunta = new Pregunta()
            //{
            //IdCuestionario = _pregunta.IdCuestionario,
            //Pregunta1 = _pregunta.Pregunta,
            //Estado = false

            //};
            //_context.Preguntas.Add(pregunta);
            //await _context.SaveChangesAsync();
            //if (pregunta.IdPregunta != 0)
            //{
            //responseAPI.EsCorrecto = true;
            //responseAPI.Valor = pregunta.IdPregunta;
            //}
            //else
            //{
            //responseAPI.EsCorrecto = true;
            //responseAPI.mensaje = "No guardado";
            //}
            //}
            //catch(Exception ex)
            //{
            //responseAPI.EsCorrecto = false;
            //responseAPI.mensaje = "No guardado "  +ex.Message;

            //}
            //return Ok(responseAPI);
            // DELETE: api/Preguntas/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePregunta(int id)
            {
            //    var responseAPI = new ResponseAPI<int>();
            //try
            //{
            //    var pregunta = await _context.Preguntas.FindAsync(id);
            //    if(pregunta != null )
            //    {
            //        _context.Remove(pregunta);
            //        await _context.SaveChangesAsync();
            //        responseAPI.EsCorrecto = true;
            //    }
            //    else
            //    {
            //        responseAPI.EsCorrecto = false;
            //        responseAPI.mensaje = "Pregunta no encontrada";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    responseAPI.EsCorrecto = false;
            //    responseAPI.mensaje = "Error generado por "  +ex.Message;
            //}
            return Ok();  
            }
        }
}
