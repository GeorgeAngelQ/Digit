using System.ComponentModel.DataAnnotations;

namespace Digitalizacion.UI.Models
{
    public class UsuarioSistemaModel
    {
        [Key]
        public int IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public string? Contrasenia { get; set; }
        public string? Rol { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
