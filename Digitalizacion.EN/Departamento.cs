using System.ComponentModel.DataAnnotations;

namespace Digitalizacion.EN
{
    public class Departamento
    {
        [Key]
        public int IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }
        public string? UbicacionDepartamento { get; set; }
        public string? ExtensionDepartamento { get; set; }
    }
}
