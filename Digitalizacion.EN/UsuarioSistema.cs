using System.ComponentModel.DataAnnotations;

namespace Digitalizacion.EN
{
    public class UsuarioSistema
    {
        [Key]
        public int IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public string? Contrasenia { get; set; }
        public string? Rol { get; set; }
    }
}
