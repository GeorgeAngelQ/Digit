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
        [Route("select-by-id/{idEquipoDigitalizacion}")]
        public EquipoDigitalizacion SelectById(int idEquipoDigitalizacion)
        {
            var blEquipoDigitalizacion = new EquipoDigitalizacionLN();
            EquipoDigitalizacion? enEquipoDigitalizacion;
            enEquipoDigitalizacion = blEquipoDigitalizacion.SelectById(idEquipoDigitalizacion);
            return enEquipoDigitalizacion;
        }
        [HttpPut]
        [Route("update/{idEquipoDigitalizacion}")]
        public void Update(int idEquipoDigitalizacion, EquipoDigitalizacion enEquipoDigitalizacion)
        {
            var blEquipoDigitalizacion = new EquipoDigitalizacionLN();
            blEquipoDigitalizacion.Update(idEquipoDigitalizacion, enEquipoDigitalizacion);
        }
        [HttpDelete]
        [Route("delete/{idEquipoDigitalizacion}")]
        public void Delete(int idEquipoDigitalizacion)
        {
            var blEquipoDigitalizacion = new EquipoDigitalizacionLN();
            blEquipoDigitalizacion.Delete(idEquipoDigitalizacion);
        }
    }
}
