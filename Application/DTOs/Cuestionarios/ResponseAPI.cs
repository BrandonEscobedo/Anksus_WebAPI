﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAnskus.Shared
{
    public class ResponseAPI<T>
    {
        public bool EsCorrecto {  get; set; }
        public T? Valor { get; set; }
        public string? mensaje { get; set; }
    }
}
