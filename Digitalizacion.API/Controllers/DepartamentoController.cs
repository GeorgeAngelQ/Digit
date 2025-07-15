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
        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            try
            {
                var blDepartamento = new DepartamentoLN();
                var list = blDepartamento.List();

                if (list == null || !list.Any())
                    return NotFound("No se encontraron departamentos");

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("pagination")]
        public ActionResult<List<ExpandoObject>> Pagination(
            [FromQuery] string? texto,
            [FromQuery] int pageSize = 5,
            [FromQuery] int currentPage = 1,
            [FromQuery] string orderBy = "IdDepartamento",
            [FromQuery] bool? sortOrder = true)
        {
            try
            {
                var departamentoLN = new DepartamentoLN();
                var data = departamentoLN.Pagination(texto ?? "", pageSize, currentPage, orderBy, sortOrder);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error en el servidor", detalle = ex.Message });
            }
        }
    }
}
