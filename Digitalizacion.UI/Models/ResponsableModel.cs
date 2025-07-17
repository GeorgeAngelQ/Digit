using System.ComponentModel.DataAnnotations;

namespace Digitalizacion.UI.Models
{
    public class ResponsableModel
    {
        [Key]
        public int IdResponsable { get; set; }
        public string? NombreResponsable { get; set; }
        public string? ApellidoResponsable { get; set; }
        public string? CorreoResponsable { get; set; }
        public string? TelefonoResponsable { get; set; }
        public string? CargoResponsable { get; set; }
    }
}
