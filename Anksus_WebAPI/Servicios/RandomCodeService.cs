using anskus.Infrastructure.Data;

namespace Anksus_WebAPI.Server.Servicios
{
    public class RandomCodeService
    {
        private readonly TestAnskusContext _context;

        public RandomCodeService(TestAnskusContext context)
        {
            _context = context;
        }
        private async Task<bool> IsCodeValid(int code)
        {
            if (_context.CuestionarioActivos.Where(x => x.Codigo == code).Any() == false)
                return true;
            else
                return false;
        }
        private async Task<int> GenerarCodigoAleatorio(Random random, int longitud)
        {
            int[] codigo = new int[longitud];
            for (int i = 0; i < longitud; i++)
            {
                codigo[i] = random.Next(10);
            }
            int cadena = Convert.ToInt32(string.Join("", codigo));
            return cadena;
        }
        public async Task<int> GenerarCodigo()
        {
            Random random = new Random();
            int codigo = await GenerarCodigoAleatorio(random, 4);
            do
            {
                codigo = await GenerarCodigoAleatorio(random, 4);
            }
            while (_context.CuestionarioActivos.Where(x => x.Codigo == codigo).Any());
            return codigo;
        }
    }
}
