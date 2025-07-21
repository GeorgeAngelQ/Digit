using System.ComponentModel.DataAnnotations;

namespace Digitalizacion.UI.Models
{
    public class DocumentoModel
    {
        [Key]
        public int IdDocumento { get; set; }
        public int IdProceso { get; set; }
        public string NombreDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string FormatoDocumento { get; set; }
        public DateTime FechaDigitalizacion { get; set; }
        public string Estado_Documento { get; set; }
        public string? RutaArchivo { get; set; }
        public string NombreOriginal { get; set; }
    }
}
