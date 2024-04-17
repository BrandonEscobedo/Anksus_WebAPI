using anskus.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.DTOs
{
    public record ResponseAPi<T>(T? Valor, bool Flag = false, string Message = null!);
}
