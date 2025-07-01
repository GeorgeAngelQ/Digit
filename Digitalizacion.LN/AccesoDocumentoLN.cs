using Digitalizacion.DA;
using Digitalizacion.EN;

namespace Digitalizacion.LN
{
    public class AccesoDocumentoLN
    {
        public void Insert(AccesoDocumento enAccesoDocumento)
        {
            var accesoDocumentoDA = new AccesoDocumentoDA();
            accesoDocumentoDA.Insert(enAccesoDocumento);
        }
        public AccesoDocumento? SelectById(int idAcceso)
        {
            var accesoDocumentoDA = new AccesoDocumentoDA();
            AccesoDocumento? enAcceso = accesoDocumentoDA.SelectById(idAcceso);
            return enAcceso;
        }
        public void Update(int idAcceso, AccesoDocumento enAccesoDocumento)
        {
            var accesoDocumentoDA = new AccesoDocumentoDA();
            accesoDocumentoDA.Update(idAcceso, enAccesoDocumento);
        }
        public void Delete(int idAcceso)
        {
            var accesoDocumentoDA = new AccesoDocumentoDA();
            accesoDocumentoDA.Delete(idAcceso);
        }
    }
}