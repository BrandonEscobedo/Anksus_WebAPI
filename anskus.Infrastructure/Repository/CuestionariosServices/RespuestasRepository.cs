using anskus.Domain.Cuestionarios;
using anskus.Domain.Models.dbModels;
using anskus.Infrastructure.Data;
namespace anskus.Infrastructure.Repository.CuestionariosServices
{
    internal sealed class RespuestasRepository : IRespuestasRepository
    {
        private readonly TestAnskusContext context;
        public RespuestasRepository(TestAnskusContext context)
        {
            this.context = context;
        }
        public async Task<bool> Add(List<Respuesta> respuestas)
        {
            if (respuestas != null)
            {
                context.Respuestas.AddRange(respuestas);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
