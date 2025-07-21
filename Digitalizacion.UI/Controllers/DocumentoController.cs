using System.Net.Http.Headers;
using AutoMapper;
using Digitalizacion.EN;
using Digitalizacion.UI.Filters;
using Digitalizacion.UI.Models;
using Libreria;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Digitalizacion.UI.Controllers
{
    [AuthorizeSession]
    [Route("mantenimiento/documento")]
    public class DocumentoController : Controller
    {
        private readonly IMapper _mapper;

        public DocumentoController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
        }

        [HttpGet]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.Mensaje = "No hay documentos registrados";
            return View();
        }

        [HttpGet]
        [Route("nuevo")]
        public async Task<IActionResult> Nuevo()
        {
            await CargarCombos();
            ViewBag.Mensaje = "Crear nuevo documento";

            return View();
        }

        [HttpGet]
        [Route("editar/{idDocumento}")]
        public async Task<IActionResult> Editar(int idDocumento)
        {
            DocumentoModel dtoDocumento = await SelectById(idDocumento);
            await CargarCombos();

            ViewBag.IdDocumento = idDocumento;
            ViewBag.Mensaje = "Se editará el documento";
            ViewBag.Documento = dtoDocumento;
            return View();
        }

        private async Task CargarCombos()
        {
            ViewBag.Procesos = await GetSelectListProcesos();
        }


        private async Task<List<SelectListItem>> GetSelectListProcesos()
        {
            using var client = new HttpClient();
            var baseUrl = ConfigurationJson.GetAppSettings("DigitalizacionApi");
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.GetAsync("api/proceso/list");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            var data = JArray.Parse(content);

            var lista = new List<SelectListItem>();

            foreach (JObject item in data)
            {
                string idProceso = item["idProceso"]?.ToString();
                string nombreResponsable = item["nombreResponsable"]?.ToString();
                string nombreDepartamento = item["nombreDepartamento"]?.ToString();
                string marcaEquipo = item["marcaEquipo"]?.ToString();
                string modeloEquipo = item["modeloEquipo"]?.ToString();
                string estado = item["estado"]?.ToString();
                string prioridad = item["prioridad"]?.ToString();

                if (!string.IsNullOrEmpty(idProceso))
                {
                    string texto = $"Proceso #{idProceso} - {nombreResponsable} - {nombreDepartamento} - {marcaEquipo} {modeloEquipo} - {estado} - {prioridad}";
                    lista.Add(new SelectListItem
                    {
                        Value = idProceso,
                        Text = texto
                    });
                }
            }

            return lista;
        }


        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Insert(IFormFile Archivo, [FromForm] DocumentoModel model)
        {
            try
            {
                if (Archivo == null || Archivo.Length == 0)
                    return BadRequest("Archivo no válido.");

                var nombreOriginal = Path.GetFileName(Archivo.FileName);
                var nombreUnico = $"{Guid.NewGuid()}{Path.GetExtension(nombreOriginal)}";
                var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Digitalizados");

                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);

                var rutaCompleta = Path.Combine(rutaCarpeta, nombreUnico);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await Archivo.CopyToAsync(stream);
                }

                var be = _mapper.Map<Documento>(model);
                be.NombreOriginal = nombreOriginal;
                be.RutaArchivo = $"/Digitalizados/{nombreUnico}";

                using var client = new HttpClient();
                var apiUrl = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(be), Encoding.UTF8, "application/json");
                var result = await client.PostAsync("api/documento/insert", content);

                if (!result.IsSuccessStatusCode)
                    return StatusCode((int)result.StatusCode);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }


        [HttpGet("select-by-id/{idDocumento}")]
        public async Task<DocumentoModel> SelectById(int idDocumento)
        {
            using var client = new HttpClient();
            var apiUrl = ConfigurationJson.GetAppSettings("DigitalizacionApi");
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.GetAsync("api/documento/select-by-id/" + idDocumento);
            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadAsStringAsync();
            var be = JsonConvert.DeserializeObject<Documento>(response);
            return _mapper.Map<DocumentoModel>(be);
        }

        [HttpPut("update/{idDocumento}")]
        public async Task Update(int idDocumento, string documento)
        {
            try
            {
                var dto = JsonConvert.DeserializeObject<DocumentoModel>(documento);
                var be = _mapper.Map<Documento>(dto);

                using var client = new HttpClient();
                var apiUrl = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(be), System.Text.Encoding.UTF8, "application/json");
                var result = await client.PutAsync("api/documento/update/" + idDocumento, content);

                if (!result.IsSuccessStatusCode)
                    throw new Exception(result.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("delete/{idDocumento}")]
        public async Task Delete(int idDocumento)
        {
            using var client = new HttpClient();
            var apiUrl = ConfigurationJson.GetAppSettings("DigitalizacionApi");
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.DeleteAsync("api/documento/delete/" + idDocumento);
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.StatusCode.ToString());
        }
    }
}
