using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalizacion.EN
{
    public class DocumentoDTO
    {
        public int IdDocumento { get; set; }
        public int IdProceso { get; set; }
        public string NombreDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string FormatoDocumento { get; set; }
        public DateTime FechaDigitalizacion { get; set; }
        public string Estado_Documento { get; set; }

        public string Prioridad { get; set; }

        public int TotalRegistros { get; set; }
    }
}
