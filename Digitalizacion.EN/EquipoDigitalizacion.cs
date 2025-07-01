using System.ComponentModel.DataAnnotations;

namespace Digitalizacion.EN
{
    public class EquipoDigitalizacion
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
