using System.Dynamic;
using Digitalizacion.EN;
using Digitalizacion.LN;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Digitalizacion.API.Controllers
{
    [ApiController]
    [Route("api/departamento")]
    public class DepartamentoController : ControllerBase
    {
        private static readonly Logger MainLogger = LogManager.GetCurrentClassLogger();
        [HttpPost]
        [Route("insert")]
        public void Insert(Departamento enDepartamento)
        {
            var blDepartamento = new DepartamentoLN();
            try
            {
                blDepartamento.Insert(enDepartamento);
            }
            catch (Exception ex)
            {
                dynamic ExceptionParams = new ExpandoObject();
                ExceptionParams.Exception = ex;
                ExceptionParams.Method = nameof(Insert);
                ExceptionParams.Class = nameof(DepartamentoController);
                MainLogger.Error(ExceptionParams);
            }
        }
        [HttpGet]
        [Route("select-by-id/{idDepartamento}")]
        public Departamento SelectById(int idDepartamento)
        {
            var blDepartamento = new DepartamentoLN();
            Departamento? enDepartamento;
            enDepartamento = blDepartamento.SelectById(idDepartamento);
            return enDepartamento;
        }
        [HttpPut]
        [Route("update/{idDepartamento}")]
        public void Update(int idDepartamento, Departamento enDepartamento)
        {
            var blDepartamento = new DepartamentoLN();
            blDepartamento.Update(idDepartamento, enDepartamento);
        }
        [HttpDelete]
        [Route("delete/{idDepartamento}")]
        public void Delete(int idDepartamento)
        {
            var blDepartamento = new DepartamentoLN();
            blDepartamento.Delete(idDepartamento);
        }
    }
}
