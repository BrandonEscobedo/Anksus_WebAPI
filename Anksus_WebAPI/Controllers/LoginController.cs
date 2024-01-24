using Anksus_WebAPI.Models.dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestAnskus.Shared.AutorizacionDTO;
namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AplicationUser> _signInManager;
        private readonly UserManager<AplicationUser> _userManager;
        public LoginController(IConfiguration configuration,
            SignInManager<AplicationUser> signInManager,
            UserManager<AplicationUser> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModelA login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email!, login.Password!, false, false);
            if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Username And Password Are invalid." });
            var user = await _userManager.FindByEmailAsync(login.Email!);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,login.Email!),
                new Claim("Id","1")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]!));
            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));
        
       
            var token = new JwtSecurityToken
                (
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires:expiry,
                signingCredentials:creds
                );         
            return Ok(new LoginResult { Successful=true,Token= new JwtSecurityTokenHandler().WriteToken(token)});
        }
    }
}
