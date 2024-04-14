using Anksus_WebAPI.Models.dbModels;
using anskus.Application.Contracts;
using anskus.Application.DTOs.Request.Account;
using anskus.Application.DTOs.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using TestAnskus.Shared;
using TestAnskus.Shared.AutorizacionDTO;
namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController(IAccountUser account) : ControllerBase
    {
      

        [HttpPost("identity/create")]
        public async Task<ActionResult<GeneralResponse>> Crear(CreateAccountDTO model)
        {
            if (!ModelState.IsValid) 
                return BadRequest("El modelo no puede ser nulo");

            return Ok(await account.CreateAccountAsync(model));

        } [HttpPost("identity/Login")]
        public async Task<ActionResult<GeneralResponse>> LoginAccount(LoginDTO model)
        {
            if (!ModelState.IsValid) 
                return BadRequest("El modelo no puede ser nulo");

            return Ok(await account.LoginAccountAsync(model));

        }  
    [HttpPost("identity/Refresh-token")]
        public async Task<ActionResult<GeneralResponse>> RefreshTOken(RefreshTokenDTO model)
        {
            if (!ModelState.IsValid) 
                return BadRequest("El modelo no puede ser nulo");

            return Ok(await account.RefreshTokenAsync(model));

        }

    }
}
