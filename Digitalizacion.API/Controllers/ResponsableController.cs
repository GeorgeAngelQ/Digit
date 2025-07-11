using System.Dynamic;
using Digitalizacion.EN;
using Digitalizacion.LN;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Digitalizacion.API.Controllers
{
    [ApiController]
    [Route("api/responsable")]
    public class ResponsableController : ControllerBase
    {
        private static readonly Logger MainLogger = LogManager.GetCurrentClassLogger();
        [HttpPost]
        [Route("insert")]
        public void Insert(Responsable enResponsable)
        {
            var blResponsable = new ResponsableLN();
            try
            {
                blResponsable.Insert(enResponsable);
            }
            catch (Exception ex)
            {
                dynamic ExceptionParams = new ExpandoObject();
                ExceptionParams.Exception = ex;
                ExceptionParams.Method = nameof(Insert);
                ExceptionParams.Class = nameof(ResponsableController);
                MainLogger.Error(ExceptionParams);
            }
        }
        [HttpGet]
        [Route("select-by-id/{idResponsable}")]
        public Responsable SelectById(int idResponsable)
        {
            var blResponsable = new ResponsableLN();
            Responsable? enResponsable;
            enResponsable = blResponsable.SelectById(idResponsable);
            return enResponsable;
        }
        [HttpPut]
        [Route("update/{idResponsable}")]
        public void Update(int idResponsable, Responsable enResponsable)
        {
            var blResponsable = new ResponsableLN();
            blResponsable.Update(idResponsable, enResponsable);
        }
        [HttpDelete]
        [Route("delete/{idResponsable}")]
        public void Delete(int idResponsable)
        {
            var blResponsable = new ResponsableLN();
            blResponsable.Delete(idResponsable);
        }
    }
}
