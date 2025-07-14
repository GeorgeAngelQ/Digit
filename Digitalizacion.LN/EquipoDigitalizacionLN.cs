using System.Dynamic;
using Digitalizacion.DA;
using Digitalizacion.EN;

namespace Digitalizacion.LN
{
    public class EquipoDigitalizacionLN
    {
        public void Insert(EquipoDigitalizacion enEquipoDigitalizacion)
        {
            var equipoDigitalizacionDA = new EquipoDigitalizacionDA();
            equipoDigitalizacionDA.Insert(enEquipoDigitalizacion);
        }
        public EquipoDigitalizacion? SelectById(int idEquipo)
        {
            var equipoDigitalizacionDA = new EquipoDigitalizacionDA();
            EquipoDigitalizacion? enEquipoDigitalizacion;
            enEquipoDigitalizacion = equipoDigitalizacionDA.SelectById(idEquipo);
            return enEquipoDigitalizacion;
        }
        public void Update(int idEquipo, EquipoDigitalizacion enEquipoDigitalizacion)
        {
            var equipoDigitalizacionDA = new EquipoDigitalizacionDA();
            equipoDigitalizacionDA.Update(idEquipo, enEquipoDigitalizacion);
        }
        public void Delete(int idEquipo)
        {
            var equipoDigitalizacionDA = new EquipoDigitalizacionDA();
            equipoDigitalizacionDA.Delete(idEquipo);
        }
        public List<EquipoDigitalizacion> List()
        {
            var equipoDigitalizacionDA = new EquipoDigitalizacionDA();
            return equipoDigitalizacionDA.List();
        }
        public List<ExpandoObject> Pagination(string texto, int pageSize, int currentPage, string orderBy, bool? sortOrder)
        {
            var equipoDA = new EquipoDigitalizacionDA();
            return equipoDA.Pagination(texto, pageSize, currentPage, orderBy, sortOrder);
        }
    }
}