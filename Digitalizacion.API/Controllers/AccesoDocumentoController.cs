using System.Dynamic;
using Digitalizacion.EN;
using Digitalizacion.LN;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Digitalizacion.API.Controllers
{
    [ApiController]
    [Route("api/accesodocumento")]
    public class AccesoDocumentoController : ControllerBase
    {
        private static readonly Logger MainLogger = LogManager.GetCurrentClassLogger();
        [HttpPost]
        [Route("insert")]
        public void Insert(AccesoDocumento enAccesoDocumento)
        {
            var blAccesoDocumento = new AccesoDocumentoLN();
            try
            {
                blAccesoDocumento.Insert(enAccesoDocumento);
            }
            catch (Exception ex)
            {
                dynamic ExceptionParams = new ExpandoObject();
                ExceptionParams.Exception = ex;
                ExceptionParams.Method = nameof(Insert);
                ExceptionParams.Class = nameof(AccesoDocumentoController);
                MainLogger.Error(ExceptionParams);
            }
        }
        [HttpGet]
        [Route("select-by-id/{idAccesoDocumento}")]
        public AccesoDocumento SelectById(int idAccesoDocumento)
        {
            var blAccesoDocumento = new AccesoDocumentoLN();
            AccesoDocumento? enAccesoDocumento;
            enAccesoDocumento = blAccesoDocumento.SelectById(idAccesoDocumento);
            return enAccesoDocumento;
        }
        [HttpPut]
        [Route("update/{idAccesoDocumento}")]
        public void Update(int idAccesoDocumento, AccesoDocumento enAccesoDocumento)
        {
            var blAccesoDocumento = new AccesoDocumentoLN();
            blAccesoDocumento.Update(idAccesoDocumento, enAccesoDocumento);
        }
        [HttpDelete]
        [Route("delete/{idAccesoDocumento}")]
        public void Delete(int idAccesoDocumento)
        {
            var blAccesoDocumento = new AccesoDocumentoLN();
            blAccesoDocumento.Delete(idAccesoDocumento);
        }
    }
}
