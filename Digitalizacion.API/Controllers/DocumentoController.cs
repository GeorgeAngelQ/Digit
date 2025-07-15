using System.Dynamic;
using Digitalizacion.EN;
using Digitalizacion.LN;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Digitalizacion.API.Controllers
{
    [ApiController]
    [Route("api/documento")]
    public class DocumentoController : ControllerBase
    {
        private static readonly Logger MainLogger = LogManager.GetCurrentClassLogger();
        [HttpPost]
        [Route("insert")]
        public void Insert(Documento enDocumento)
        {
            var blDocumento = new DocumentoLN();
            try
            {
                blDocumento.Insert(enDocumento);
            }
            catch (Exception ex)
            {
                dynamic ExceptionParams = new ExpandoObject();
                ExceptionParams.Exception = ex;
                ExceptionParams.Method = nameof(Insert);
                ExceptionParams.Class = nameof(DocumentoController);
                MainLogger.Error(ExceptionParams);
            }
        }
        [HttpGet]
        [Route("select-by-id/{idDocumento}")]
        public Documento SelectById(int idDocumento)
        {
            var blDocumento = new DocumentoLN();
            Documento? enDocumento;
            enDocumento = blDocumento.SelectById(idDocumento);
            return enDocumento;
        }
        [HttpPut]
        [Route("update/{idDocumento}")]
        public void Update(int idDocumento, Documento enDocumento)
        {
            var blDocumento = new DocumentoLN();
            blDocumento.Update(idDocumento, enDocumento);
        }
        [HttpDelete]
        [Route("delete/{idDocumento}")]
        public void Delete(int idDocumento)
        {
            var blDocumento = new DocumentoLN();
            blDocumento.Delete(idDocumento);
        }
        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            try
            {
                var blDocumento = new DocumentoLN();
                var list = blDocumento.List();

                if (list == null || !list.Any())
                    return NotFound("No se encontraron responsables");

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
            [FromQuery] string orderBy = "IdDocumento",
            [FromQuery] bool? sortOrder = true)
        {
            try
            {
                var documentoLN = new DocumentoLN();
                var data = documentoLN.Pagination(texto ?? "", pageSize, currentPage, orderBy, sortOrder);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error en el servidor", detalle = ex.Message });
            }
        }
    }
}
