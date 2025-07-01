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
        public EquipoDigitalizacion? SelectById(int idEquipoDigitalizacion)
        {
            var equipoDigitalizacionDA = new EquipoDigitalizacionDA();
            EquipoDigitalizacion? enEquipoDigitalizacion;
            enEquipoDigitalizacion = equipoDigitalizacionDA.SelectById(idEquipoDigitalizacion);
            return enEquipoDigitalizacion;
        }
        public void Update(int idEquipoDigitalizacion, EquipoDigitalizacion enEquipoDigitalizacion)
        {
            var equipoDigitalizacionDA = new EquipoDigitalizacionDA();
            equipoDigitalizacionDA.Update(idEquipoDigitalizacion, enEquipoDigitalizacion);
        }
        public void Delete(int idEquipoDigitalizacion)
        {
            var equipoDigitalizacionDA = new EquipoDigitalizacionDA();
            equipoDigitalizacionDA.Delete(idEquipoDigitalizacion);
        }
    }
}