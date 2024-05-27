using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.DTOs.Cuestionarios
{
    public class CategoriasDTO
    {
       
        [Column("idCategoria")]
        public int IdCategoria { get; set; }

        [Column("categoria")]
        [StringLength(100)]
        public string Categoria { get; set; } = null!;

    
    }
}
