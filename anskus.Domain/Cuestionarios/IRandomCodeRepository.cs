using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public interface IRandomCodeRepository
    {
        public Task<int> GenerarCodigo();
        Task<bool> IsCodeValid(int code);

    }
}
