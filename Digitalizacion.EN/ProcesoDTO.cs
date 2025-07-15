namespace Digitalizacion.EN
{
    public class ProcesoDTO
    {
        public int IdProceso { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; }
        public string Prioridad { get; set; }

        public int IdResponsable { get; set; }
        public string NombreResponsable { get; set; }

        public int IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; }

        public int IdEquipo { get; set; }
        public string MarcaEquipo { get; set; }
        public string ModeloEquipo { get; set; }

        public int TotalRegistros { get; set; }
    }
}
