using anskus.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.DTOs
{
    public class ResponseAPi<T>
    {
        public T? Valor { get; set; }
        public GeneralResponse? response { get; set; }

    }
}
