using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using anskus.Application.DTOs.Cuestionarios;
using AutoMapper;
using anskus.Application.Services;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Cuestionarios;
using MediatR;
using anskus.Application.Preguntas.Create;
using anskus.Application.Preguntas.Delete;
using anskus.Application.Preguntas.Update;

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/Preguntas")]
    [ApiController]
    public class PreguntasController(ISender _mediator) : ControllerBase
    {
    

        // GET: api/Preguntas

        // GET: api/Preguntas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPregunta(int id)
        {
        
            return Ok();
        }

        // PUT: api/Preguntas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutPregunta( PreguntasDTO PreguntaDTO)
        {

            if (PreguntaDTO != null)
            {
                var result = await _mediator.Send(new UpdatePreguntaCommand(PreguntaDTO));
                return Ok(result);
            }
            return BadRequest();
        }

        // POST: api/Preguntas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreatePreguntas(PreguntasDTO _pregunta)
        {
            if (User.Identity!.IsAuthenticated == true)
            {
              var result=await  _mediator.Send(new CreatePreguntaCommand(  _pregunta));
                return Ok(result);
            }
            return BadRequest();


        }
           
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePregunta(int id)
            {
                if(User.Identity!.IsAuthenticated == true)
            {
            var result =await _mediator.Send(new DeletePreguntaCommand(  id));
            return Ok(result);
            }
                return BadRequest();
        }
        }
}
