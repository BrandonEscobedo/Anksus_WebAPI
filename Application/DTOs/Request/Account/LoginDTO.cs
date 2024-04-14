using System.ComponentModel.DataAnnotations;
namespace anskus.Application.DTOs.Request.Account
{
    public class LoginDTO
    {
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
