using Digitalizacion.DA;
using Digitalizacion.EN;

namespace Digitalizacion.LN
{
    public class DocumentoLN
    {
        public void Insert(Documento enDocumento)
        {
            var documentoDA = new DocumentoDA();
            documentoDA.Insert(enDocumento);
        }
        public Documento? SelectById(int idDocumento)
        {
            var documentoDA = new DocumentoDA();
            Documento? enDocumento = documentoDA.SelectById(idDocumento);
            return enDocumento;
        }
        public void Update(int idDocumento, Documento enDocumento)
        {
            var documentoDA = new DocumentoDA();
            documentoDA.Update(idDocumento, enDocumento);
        }
        public void Delete(int idDocumento)
        {
            var documentoDA = new DocumentoDA();
            documentoDA.Delete(idDocumento);
        }
    }
}