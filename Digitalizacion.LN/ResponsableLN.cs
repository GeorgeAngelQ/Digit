using Digitalizacion.DA;
using Digitalizacion.EN;

namespace Digitalizacion.LN
{
    public class ResponsableLN
    {
        public void Insert(Responsable enResponsable)
        {
            var responsableDA = new ResponsableDA();
            responsableDA.Insert(enResponsable);
        }
        public Responsable? SelectById(int idResponsable)
        {
            var responsableDA = new ResponsableDA();
            Responsable? enResponsable;
            enResponsable = responsableDA.SelectById(idResponsable);
            return enResponsable;
        }
        public void Update(int idResponsable, Responsable enResponsable)
        {
            var responsableDA = new ResponsableDA();
            responsableDA.Update(idResponsable, enResponsable);
        }
        public void Delete(int idResponsable)
        {
            var responsableDA = new ResponsableDA();
            responsableDA.Delete(idResponsable);
        }
    }
}