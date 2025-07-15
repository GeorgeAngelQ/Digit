using System.Dynamic;
using Digitalizacion.EN;
using Digitalizacion.LN;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Digitalizacion.API.Controllers
{
    [ApiController]
    [Route("api/usuariosistema")]
    public class UsuarioSistemaController : ControllerBase
    {
        private static readonly Logger MainLogger = LogManager.GetCurrentClassLogger();
        [HttpPost]
        [Route("insert")]
        public void Insert(UsuarioSistema enUsuarioSistema)
        {
            var blUsuarioSistema = new UsuarioSistemaLN();
            try
            {
                blUsuarioSistema.Insert(enUsuarioSistema);
            }
            catch (Exception ex)
            {
                dynamic ExceptionParams = new ExpandoObject();
                ExceptionParams.Exception = ex;
                ExceptionParams.Method = nameof(Insert);
                ExceptionParams.Class = nameof(UsuarioSistemaController);
                MainLogger.Error(ExceptionParams);
            }
        }

        [HttpGet]
        [Route("select-by-id/{idUsuario}")]
        public UsuarioSistema SelectById(int idUsuario)
        {
            var blUsuarioSistema = new UsuarioSistemaLN();
            UsuarioSistema? enUsuarioSistema;
            enUsuarioSistema = blUsuarioSistema.SelectById(idUsuario);
            return enUsuarioSistema;
        }
        [HttpPut]
        [Route("update/{idUsuario}")]
        public void Update(int idUsuario, UsuarioSistema enUsuarioSistema)
        {
            var blUsuarioSistema = new UsuarioSistemaLN();
            blUsuarioSistema.Update(idUsuario, enUsuarioSistema);
        }
        [HttpDelete]
        [Route("delete/{idUsuario}")]
        public void Delete(int idUsuario)
        {
            var blUsuarioSistema = new UsuarioSistemaLN();
            blUsuarioSistema.Delete(idUsuario);
        }
        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            try
            {
                var blUsuarioSistema = new UsuarioSistemaLN();
                var list = blUsuarioSistema.List();

                if (list == null || !list.Any())
                    return NotFound("No se encontraron usuarios");

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
            [FromQuery] string orderBy = "IdUsuario",
            [FromQuery] bool? sortOrder = true)
        {
            try
            {
                var usuarioSistemaLN = new UsuarioSistemaLN();
                var data = usuarioSistemaLN.Pagination(texto ?? "", pageSize, currentPage, orderBy, sortOrder);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error en el servidor", detalle = ex.Message });
            }
        }
    }
}