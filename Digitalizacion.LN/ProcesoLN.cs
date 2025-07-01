using Digitalizacion.DA;
using Digitalizacion.EN;

namespace Digitalizacion.LN
{
    public class ProcesoLN
    {
        public void Insert(Proceso enProceso)
        {
            var procesoDA = new ProcesoDA();
            procesoDA.Insert(enProceso);
        }
        public Proceso? SelectById(int idProceso)
        {
            var procesoDA = new ProcesoDA();
            Proceso? enProceso = procesoDA.SelectById(idProceso);
            return enProceso;
        }
        public void Update(int idProceso, Proceso enProceso)
        {
            var procesoDA = new ProcesoDA();
            procesoDA.Update(idProceso, enProceso);
        }
        public void Delete(int idProceso)
        {
            var procesoDA = new ProcesoDA();
            procesoDA.Delete(idProceso);
        }
    }
}