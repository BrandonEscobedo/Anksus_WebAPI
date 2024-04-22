using anskus.Domain.Models.dbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace anskus.Domain.Authentication;

[Index("IdImagenPerfil", Name = "IX_AspNetUsers_id_imagen_perfil")]
public class ApplicationUser:IdentityUser
{
   public string? Name { get; set; }

    [Column("id_imagen_perfil")]

    public int IdImagenPerfil { get; set; } = 1;


    [ForeignKey("IdImagenPerfil")]
    public virtual ImagenesPerfil IdImagenPerfilNavigation { get; set; } = null!;
    [InverseProperty("IdUsuarioNavigation")]
    public virtual CuestionarioActivo? CuestionarioActivo { get; set; }
    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection< Cuestionario>? Cuestionario { get; set; }

}
