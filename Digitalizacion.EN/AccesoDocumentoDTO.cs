using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalizacion.EN
{
    public class AccesoDocumentoDTO
    {
        public int IdAcceso { get; set; }
        public int IdDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string TipoDocumento { get; set; }

        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string NombreCompleto { get; set; }

        public DateTime FechaAcceso { get; set; }
        public string TipoAcceso { get; set; }

        public int TotalRegistros { get; set; }
    }
}
