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

namespace Digitalizacion.UI.Controllers
{
    [AuthorizeSession]
    [Route("mantenimiento/proceso")]
    public class ProcesoController : Controller
    {
        private IMapper _mapper;

        public ProcesoController()
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
            ViewBag.Mensaje = "No hay procesos registrados";
            return View();
        }

        [HttpGet]
        [Route("nuevo")]
        public async Task<IActionResult> Nuevo()
        {
            await CargarCombos();
            ViewBag.Mensaje = "Crear nuevo proceso";
            return View();
        }

        [HttpGet]
        [Route("editar/{idProceso}")]
        public async Task<IActionResult> Editar(int idProceso)
        {
            ProcesoModel dtoProceso = await SelectById(idProceso);
            await CargarCombos();

            ViewBag.IdProceso = idProceso;
            ViewBag.Mensaje = "Se editará el proceso";
            ViewBag.Proceso = dtoProceso;
            return View();
        }

        private async Task CargarCombos()
        {
            ViewBag.Responsables = await GetSelectList("responsable", "IdResponsable", "NombreResponsable");
            ViewBag.Departamentos = await GetSelectList("departamento", "IdDepartamento", "NombreDepartamento");
            ViewBag.Equipos = await GetSelectList("equipodigitalizacion", "IdEquipo", "MarcaEquipo");
        }


        private async Task<List<SelectListItem>> GetSelectList(string api, string valueField, string textField)
        {
            using var client = new HttpClient();
            var baseUrl = ConfigurationJson.GetAppSettings("DigitalizacionApi");
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.GetAsync($"api/{api}/list");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            var data = JArray.Parse(content);

            var lista = new List<SelectListItem>();

            foreach (JObject item in data)
            {
                string valor = "";
                string texto = "";

                switch (api.ToLower())
                {
                    case "responsable":
                        valor = item["idResponsable"]?.ToString();
                        texto = $"{item["nombreResponsable"]} {item["apellidoResponsable"]}";
                        break;

                    case "departamento":
                        valor = item["idDepartamento"]?.ToString();
                        texto = item["nombreDepartamento"]?.ToString();
                        break;

                    case "equipodigitalizacion":
                        valor = item["idEquipo"]?.ToString();
                        texto = $"{item["tipoEquipo"]} - {item["marcaEquipo"]}";
                        break;

                    default:
                        valor = "0";
                        texto = "Sin datos";
                        break;
                }

                System.Diagnostics.Debug.WriteLine($"[DEBUG] Item -> Value: {valor}, Text: {texto}");

                if (!string.IsNullOrEmpty(valor) && !string.IsNullOrEmpty(texto))
                {
                    lista.Add(new SelectListItem
                    {
                        Value = valor,
                        Text = texto
                    });
                }
            }

            System.Diagnostics.Debug.WriteLine($"[DEBUG] Total elementos cargados: {lista.Count}");
            return lista;
        }




        [HttpPost]
        [Route("insert")]
        public async Task Insert(string proceso)
        {
            try
            {
                var dto = JsonConvert.DeserializeObject<ProcesoModel>(proceso);
                var be = _mapper.Map<Proceso>(dto);

                using var client = new HttpClient();
                var apiUrl = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(be), System.Text.Encoding.UTF8, "application/json");
                var result = await client.PostAsync("api/proceso/insert", content);
                if (!result.IsSuccessStatusCode)
                    throw new Exception(result.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("select-by-id/{idProceso}")]
        public async Task<ProcesoModel> SelectById(int idProceso)
        {
            using var client = new HttpClient();
            var apiUrl = ConfigurationJson.GetAppSettings("DigitalizacionApi");
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.GetAsync("api/proceso/select-by-id/" + idProceso);
            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadAsStringAsync();
            var be = JsonConvert.DeserializeObject<Proceso>(response);
            return _mapper.Map<ProcesoModel>(be);
        }

        [HttpPut("update/{idProceso}")]
        public async Task Update(int idProceso, string proceso)
        {
            try
            {
                var dto = JsonConvert.DeserializeObject<ProcesoModel>(proceso);
                var be = _mapper.Map<Proceso>(dto);

                using var client = new HttpClient();
                var apiUrl = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(be), System.Text.Encoding.UTF8, "application/json");
                var result = await client.PutAsync("api/proceso/update/" + idProceso, content);
                if (!result.IsSuccessStatusCode)
                    throw new Exception(result.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("delete/{idProceso}")]
        public async Task Delete(int idProceso)
        {
            using var client = new HttpClient();
            var apiUrl = ConfigurationJson.GetAppSettings("DigitalizacionApi");
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.DeleteAsync("api/proceso/delete/" + idProceso);
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.StatusCode.ToString());
        }
    }
}
