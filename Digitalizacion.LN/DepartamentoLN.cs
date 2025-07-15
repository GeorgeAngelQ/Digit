using System.Dynamic;
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
        public List<Departamento> List()
        {
            var departamentoDA = new DepartamentoDA();
            return departamentoDA.List();
        }
        public List<ExpandoObject> Pagination(string texto, int pageSize, int currentPage, string orderBy, bool? sortOrder)
        {
            var departamentoDA = new DepartamentoDA();
            return departamentoDA.Pagination(texto, pageSize, currentPage, orderBy, sortOrder);
        }
    }
}