using anskus.Domain.Authentication;
using anskus.Domain.Cuestionarios;
using anskus.Domain.Models.dbModels;
using anskus.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
namespace anskus.Infrastructure.Repository.CuestionarioActivoServices
{
    internal sealed class CuestionarioActivoRepository(TestAnskusContext context,
        UserManager<ApplicationUser> userManager,IRandomCodeRepository randomCode) : ICuestionarioActivoRepository
    {
        public async Task<CuestionarioActivo> ActivarCuestionario(int idcuestionario, string email)
        {
                var user = userManager.FindByEmailAsync(email);
            if (user.Result != null)
            {
                var cuestionario =await context.Cuestionarios.Where
                    (x => x.IdCuestionario == idcuestionario).Include(x=>x.Pregunta)
                   .ThenInclude(x=>x.Respuesta).FirstOrDefaultAsync();
                if (cuestionario != null)
                {
                   
                    if (context.CuestionarioActivos.Where(x => x.IdCuestionario == idcuestionario && x.IdUsuario == user.Result.Id).Any()==false)
                    {
                        int codigo =await randomCode.GenerarCodigo();
                        if (codigo != 0)
                        {   CuestionarioActivo cuestionarioActivo = new CuestionarioActivo { 
                            IdCuestionario=idcuestionario
                            ,Codigo=codigo,
                            IdUsuario=user.Result.Id
                    };                          
                            context.CuestionarioActivos.Add(cuestionarioActivo);                     
                            await context.SaveChangesAsync();
                            cuestionarioActivo.cuestionario = cuestionario;
                            return cuestionarioActivo;
                        }                                    
                    }

                    //return Task.CompletedTask;
                }
            }
            return null;

        }

        public async Task<bool> IsCodeValid(int code)
        {
            return await randomCode.IsCodeValid(code);    
        }
    }
}
