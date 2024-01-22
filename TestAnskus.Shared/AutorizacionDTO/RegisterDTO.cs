using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAnskus.Shared.AutorizacionDTO
{
    internal class RegisterDTO:LoginDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required,Compare(nameof(Password)), DataType(DataType.Password)]

        public string ConfirmPassword {  get; set; } =string.Empty;

    }
}
