using System.ComponentModel.DataAnnotations;

namespace Digitalizacion.UI.Models
{
    public class EquipoDigitalizacionModel
    {
        [Key]
        public int IdEquipo { get; set; }
        public string? TipoEquipo { get; set; }
        public string? MarcaEquipo { get; set; }
        public string? ModeloEquipo { get; set; }
        public string? EstadoEquipo { get; set; }
        public string? UbicacionEquipo { get; set; }
    }
}
