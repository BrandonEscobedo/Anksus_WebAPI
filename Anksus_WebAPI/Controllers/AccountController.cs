using Anksus_WebAPI.Models.dbModels;
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
    public class AccountController : ControllerBase
    {
       public readonly UserManager<AplicationUser> _userManager;
        public AccountController(UserManager<AplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] RegisterModelA model)
        {
            var newUser = new AplicationUser { UserName = model.Email, Email = model.Email, IdImagenPerfil = 1 };
            var result = await _userManager.CreateAsync(newUser, model.Password!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return Ok(new RegisterResult { Successful = false, Errors = errors });
            }
            return Ok(new RegisterResult { Successful = true });


        }
    }
}
