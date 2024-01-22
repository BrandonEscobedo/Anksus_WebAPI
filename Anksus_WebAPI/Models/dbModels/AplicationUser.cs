using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Anksus_WebAPI.Models.dbModels
{
    [Index("IdImagenPerfil", Name = "IX_AspNetUsers_id_imagen_perfil")]

    public class AplicationUser:IdentityUser<int>
    {
        public AplicationUser()
        {
            Cuestionarios = new HashSet<Cuestionario>();
        }

        [Column("id_imagen_perfil")]

        public int IdImagenPerfil { get; set; } = 1;


        [ForeignKey("IdImagenPerfil")]
        public virtual ImagenesPerfil IdImagenPerfilNavigation { get; set; } = null!;
        [InverseProperty("IdUsuarioNavigation")]
        public virtual CuestionarioActivo? CuestionarioActivo { get; set; }


        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Cuestionario> Cuestionarios { get; set; } 
    }
}
