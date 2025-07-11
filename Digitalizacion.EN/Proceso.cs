using System.ComponentModel.DataAnnotations;

namespace Digitalizacion.EN
{
    public class Proceso
    {
        [Key]
        public int IdProceso { get; set; }
        public int IdResponsable { get; set; }
        public int IdDepartamento { get; set; }
        public int IdEquipo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Estado { get; set; }
        public string? Prioridad { get; set; }
    }
}
