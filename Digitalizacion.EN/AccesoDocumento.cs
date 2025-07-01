using System.ComponentModel.DataAnnotations;

namespace Digitalizacion.EN
{
    public class AccesoDocumento
    {
        [Key]
        public int IdAcceso { get; set; }
        public int? IdDocumento { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaAcceso { get; set; }
        public string? TipoAcceso { get; set; }
    }
}
