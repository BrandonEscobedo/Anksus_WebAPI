using anskus.Domain.Cuestionarios;
using anskus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrastructure.Repository.RandomCode
{
    internal sealed class RandomCodeRepository:IRandomCodeRepository
    {
        private readonly TestAnskusContext _context;

        public RandomCodeRepository(TestAnskusContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeValid(int code)
        {
            if (await _context.CuestionarioActivos.Where(x => x.Codigo == code).FirstOrDefaultAsync() == null)
                return false;
            else
                return true;
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
