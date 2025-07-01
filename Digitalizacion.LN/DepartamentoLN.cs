using Digitalizacion.DA;
using Digitalizacion.EN;

namespace Digitalizacion.LN
{
    public class DepartamentoLN
    {
        public void Insert(Departamento enDepartamento)
        {
            var departamentoDA = new DepartamentoDA();
            departamentoDA.Insert(enDepartamento);
        }
        public Departamento? SelectById(int idDepartamento)
        {
            var departamentoDA = new DepartamentoDA();
            Departamento? enDepartamento;
            enDepartamento = departamentoDA.SelectById(idDepartamento);
            return enDepartamento;
        }
        public void Update(int idDepartamento, Departamento enDepartamento)
        {
            var departamentoDA = new DepartamentoDA();
            departamentoDA.Update(idDepartamento, enDepartamento);
        }
        public void Delete(int idDepartamento)
        {
            var departamentoDA = new DepartamentoDA();
            departamentoDA.Delete(idDepartamento);
        }
    }
}