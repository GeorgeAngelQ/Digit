using System.Dynamic;
using Digitalizacion.EN;
using Digitalizacion.LN;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Digitalizacion.API.Controllers
{
    [ApiController]
    [Route("api/equipodigitalizacion")]
    public class EquipoDigitalizacionController : ControllerBase
    {
        private static readonly Logger MainLogger = LogManager.GetCurrentClassLogger();
        [HttpPost]
        [Route("insert")]
        public void Insert(EquipoDigitalizacion enEquipoDigitalizacion)
        {
            var blEquipoDigitalizacion = new EquipoDigitalizacionLN();
            try
            {
                blEquipoDigitalizacion.Insert(enEquipoDigitalizacion);
            }
            catch (Exception ex)
            {
                dynamic ExceptionParams = new ExpandoObject();
                ExceptionParams.Exception = ex;
                ExceptionParams.Method = nameof(Insert);
                ExceptionParams.Class = nameof(EquipoDigitalizacionController);
                MainLogger.Error(ExceptionParams);
            }
        }
        [HttpGet]
        [Route("select-by-id/{idEquipo}")]
        public EquipoDigitalizacion SelectById(int idEquipo)
        {
            var blEquipoDigitalizacion = new EquipoDigitalizacionLN();
            EquipoDigitalizacion? enEquipoDigitalizacion;
            enEquipoDigitalizacion = blEquipoDigitalizacion.SelectById(idEquipo);
            return enEquipoDigitalizacion;
        }
        [HttpPut]
        [Route("update/{idEquipo}")]
        public void Update(int idEquipo, EquipoDigitalizacion enEquipoDigitalizacion)
        {
            var blEquipoDigitalizacion = new EquipoDigitalizacionLN();
            blEquipoDigitalizacion.Update(idEquipo, enEquipoDigitalizacion);
        }
        [HttpDelete]
        [Route("delete/{idEquipo}")]
        public void Delete(int idEquipo)
        {
            var blEquipoDigitalizacion = new EquipoDigitalizacionLN();
            blEquipoDigitalizacion.Delete(idEquipo);
        }
        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            try
            {
                var blEquipoDigitalizacion = new EquipoDigitalizacionLN();
                var list = blEquipoDigitalizacion.List();

                if (list == null || !list.Any())
                    return NotFound("No se encontraron equipos");

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
            [FromQuery] string orderBy = "IdEquipo",
            [FromQuery] bool? sortOrder = true)
        {
            try
            {
                var equipoLN = new EquipoDigitalizacionLN();
                var data = equipoLN.Pagination(texto ?? "", pageSize, currentPage, orderBy, sortOrder);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error en el servidor", detalle = ex.Message });
            }
        }
    }
}
