using System.ComponentModel.DataAnnotations;

namespace Digitalizacion.UI.Models
{
    public class DepartamentoModel
    {
        [Key]
        public int IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }
        public string? UbicacionDepartamento { get; set; }
        public string? ExtensionDepartamento { get; set; }
    }
}
