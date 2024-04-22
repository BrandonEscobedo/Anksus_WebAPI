using anskus.Domain.Authentication;
using anskus.Domain.Cuestionarios;
using anskus.Domain.Models.dbModels;
using anskus.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
namespace anskus.Infrastructure.Repository.CuestionarioActivoServices
{
    internal sealed class CuestionarioActivoRepository(TestAnskusContext context,
        UserManager<ApplicationUser> userManager,IRandomCodeRepository randomCode) : ICuestionarioActivoRepository
    {
        public async Task<int> ActivarCuestionario(int idcuestionario, string email)
        {
                var user = userManager.FindByEmailAsync(email);
            if (user.Result != null)
            {
                var cuestionario = context.Cuestionarios.Where(x => x.IdCuestionario == idcuestionario).FirstOrDefault();
                if (cuestionario != null)
                {
                    if (context.CuestionarioActivos.Where(x => x.IdCuestionario == idcuestionario && x.IdUsuario == user.Result.Id).Any()==false)
                    {
                        int codigo =await randomCode.GenerarCodigo();
                        if (codigo != 0)
                        {
                            context.CuestionarioActivos.Add(new CuestionarioActivo
                            { IdCuestionario = idcuestionario, IdUsuario = user.Result.Id, Codigo = codigo });
                            await context.SaveChangesAsync();
                            return codigo;
                        }                                    
                    }

                    //return Task.CompletedTask;
                }
            }
            return 0;

        }

        public async Task<bool> IsCodeValid(int code)
        {
            return await randomCode.IsCodeValid(code);    
        }
    }
}
