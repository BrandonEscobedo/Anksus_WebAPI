using Anksus_WebAPI.Models.dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       public readonly UserManager<AplicationUser> _userManager;
        public AccountController(UserManager<AplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] RegisterModel model)
        {
            var newUser = new AplicationUser { UserName = model.Input.Email, Email = model.Input.Email };
            var result = await _userManager.CreateAsync(newUser, model.Input.Password!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return Ok(new IdentityResult {Succeeded=false,Errors=errors });
            }


        }
    }
}
