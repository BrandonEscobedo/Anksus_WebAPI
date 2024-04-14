using Anksus_WebAPI.Models.dbModels;
using anskus.Application.Contracts;
using anskus.Application.DTOs.Request.Account;
using anskus.Application.DTOs.Response;
using anskus.Domain.Entity.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace anskus.Infrastructure.Repository
{
    public class AccountRepository(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        SignInManager<ApplicationUser> signInManager, TestAnskusContext context) : IAccountUser
    {
        private async Task<ApplicationUser> FindUserByEmailAsync(string email) => await userManager!.FindByEmailAsync(email!);

        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        private string CheckResponse(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return string.Join(Environment.NewLine, errors);
            }
            return null!;
        }
        private async Task<string> GenerateToken(ApplicationUser user)
        {
            try
            {

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]!));
                var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var userClaims = new[]
                {
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim(ClaimTypes.Email,user.Email)
                };
                var expiry = DateTime.Now.AddDays(1);
                var token = new JwtSecurityToken
                    (
                    issuer: configuration["JwtIssuer"],
                    audience: configuration["JwtAudience"],
                    claims: userClaims,
                    expires: expiry,
                    signingCredentials: creds
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch { return null!; }
        }
        public async Task<GeneralResponse> CreateAccountAsync(CreateAccountDTO model)
        {
            try
            {
                if (await FindUserByEmailAsync(model.Email) != null)
                    return new GeneralResponse(false, "Este correo ya esta en uso");
                var user = new ApplicationUser()
                {
                    Name = model.Email,
                    Email=model.Email,
                    UserName = model.Email,
                    PasswordHash = model.Password
                };

                var result = await userManager.CreateAsync(user, model.Password);
                string error = CheckResponse(result);
                if (!string.IsNullOrEmpty(error))
                {
                    return new GeneralResponse(false, error);
                }
                return new GeneralResponse(true, "Se ha creado  la cuenta");
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }

        }

        public async Task<LoginResponse> LoginAccountAsync(LoginDTO model)
        {
            try
            {   
                var user = await FindUserByEmailAsync(model.Email!) ;
                if (user is null)
                    return new LoginResponse(false, "Este usuario no existe");
                SignInResult result;
                try
                {
                    result = await signInManager.CheckPasswordSignInAsync(user, model.Password!, false);

                }
                catch
                {
                    return new LoginResponse(false, "Credenciales Invalidas");

                }
                if (!result.Succeeded)
                {
                    return new LoginResponse(false, "Credenciales Invalidas");
                }
                string jwtoken = await GenerateToken(user);
                string refreshtoken = GenerateRefreshToken();
                if (string.IsNullOrEmpty(jwtoken) || string.IsNullOrEmpty(refreshtoken))
                {
                    return new LoginResponse(false, "ocurrio un error al iniciar sesion");
                }
                else
                {
                    var saveResult =await SaveRefreshToken(user.Id, refreshtoken);
                    if (saveResult.Flag)
                        return new LoginResponse(true, $"{user.Name} Se ha logeado correctamente", jwtoken, refreshtoken);

                    else
                        return new LoginResponse();
                }
                   

                   
            }
            catch (Exception ex)
            {
                return new LoginResponse(false, ex.Message);

            }
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model)
        {
            var token = await context.RefreshToken.FirstOrDefaultAsync(x => x.Token == model.Token);
            if (token == null)
                return new LoginResponse();
            var user = await userManager.FindByEmailAsync(token.UserId.ToString()!);
            string newtoken=await GenerateToken(user);
            string newRefreshToken = GenerateRefreshToken();
            var saveResult = await SaveRefreshToken(user.Id, newRefreshToken);
            if (saveResult.Flag)
            {
                return new LoginResponse(true, $"{user.Name} se ha logeado correctamente", newtoken, newRefreshToken);
                
            }
            else
                return new LoginResponse();
        }
        public async Task<GeneralResponse> SaveRefreshToken(string userId, string token)
        {
            try
            {
                var user = await context.RefreshToken.FirstOrDefaultAsync(t => t.UserId == userId);
                if (user == null)
                {
                    context.RefreshToken.Add(new RefreshToken() { UserId = userId, Token = token });

                }
                else
                    user.Token=token;
                await context.SaveChangesAsync();
                return new GeneralResponse(true, null!);
            }
            catch(Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }
        }
    }
}
