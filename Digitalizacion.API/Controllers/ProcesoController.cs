using System.Dynamic;
using Digitalizacion.EN;
using Digitalizacion.LN;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Digitalizacion.API.Controllers
{
    [ApiController]
    [Route("api/proceso")]
    public class ProcesoController : ControllerBase
    {
        private static readonly Logger MainLogger = LogManager.GetCurrentClassLogger();
        [HttpPost]
        [Route("insert")]
        public void Insert(Proceso enProceso)
        {
            var blProceso = new ProcesoLN();
            try
            {
                blProceso.Insert(enProceso);
            }
            catch (Exception ex)
            {
                dynamic ExceptionParams = new ExpandoObject();
                ExceptionParams.Exception = ex;
                ExceptionParams.Method = nameof(Insert);
                ExceptionParams.Class = nameof(ProcesoController);
                MainLogger.Error(ExceptionParams);
            }

        }
        [HttpGet]
        [Route("select-by-id/{idProceso}")]
        public Proceso SelectById(int idProceso)
        {
            var blProceso = new ProcesoLN();
            Proceso? enProceso;
            enProceso = blProceso.SelectById(idProceso);
            return enProceso;
        }
        [HttpPut]
        [Route("update/{idProceso}")]
        public void Update(int idProceso, Proceso enProceso)
        {
            var blProceso = new ProcesoLN();
            blProceso.Update(idProceso, enProceso);
        }
        [HttpDelete]
        [Route("delete/{idProceso}")]
        public void Delete(int idProceso)
        {
            var blProceso = new ProcesoLN();
            blProceso.Delete(idProceso);
        }
        [HttpGet]
        [Route("list")]
        public ActionResult<List<ProcesoDTO>> List()
        {
            try
            {
                var procesoLN = new ProcesoLN();
                var procesos = procesoLN.List();
                return Ok(procesos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("pagination")]
        public ActionResult<List<ProcesoDTO>> Pagination(
            [FromQuery] string? texto,
            [FromQuery] int pageSize = 5,
            [FromQuery] int currentPage = 1,
            [FromQuery] string orderBy = "IdProceso",
            [FromQuery] bool? sortOrder = true)
        {
            try
            {
                var procesoLN = new ProcesoLN();
                var procesos = procesoLN.Pagination(texto ?? "", pageSize, currentPage, orderBy, sortOrder);
                return Ok(procesos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
