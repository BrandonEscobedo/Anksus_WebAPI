using Microsoft.AspNetCore.Mvc;
using MediatR;
using anskus.Application.CuestionarioActivo.Commands.Create;
using System.Security.Claims;
using anskus.Application.CuestionarioActivo.Querys.VerificarCodigoCuestionario;

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/CuestionarioActivo")]
    [ApiController]
    public class CuestionarioActivoController(ISender _mediator) : ControllerBase
    {

    [HttpPost]
        public async Task<IActionResult> CreateCuestionarioActivo(int idcuest)
        {
            if (User.Identity!.IsAuthenticated)
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                if (email != null)
                {
                    var result = await _mediator.Send(new CreateCuestionarioActivoCommand(idcuest, email));
                    if (result != 0)
                    {
                        return Ok(result);
                    }
                }                   
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<bool> VerificarCodigoCuestionario(int codigo)
        {
            var response = await _mediator.Send( new VerificarCodigoQuery(codigo));
            return response;
        }

    }
}
