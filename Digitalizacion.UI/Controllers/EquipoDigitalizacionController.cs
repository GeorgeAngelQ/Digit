using System.Dynamic;
using System.Net.Http.Headers;
using AutoMapper;
using Digitalizacion.EN;
using Digitalizacion.UI.Filters;
using Digitalizacion.UI.Models;
using Libreria;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Digitalizacion.UI.Controllers
{
    [AuthorizeSession]
    [Route("mantenimiento/equipodigitalizacion")]
    public class EquipoDigitalizacionController : Controller
    {
        private IMapper _mapper;
        public EquipoDigitalizacionController()
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
            ViewBag.Mensaje = "No hay equipos de digitalización";
            return View();
        }
        [HttpGet]
        [Route("nuevo")]
        public IActionResult Nuevo()
        {
            ViewBag.Mensaje = "Crear nuevo equipo de digitalización";
            return View();
        }
        [HttpGet]
        [Route("editar/{idEquipo}")]
        public async Task<IActionResult> Editar(int idEquipo)
        {
            EquipoDigitalizacionModel dtoEquipoDigitalizacion;

            dtoEquipoDigitalizacion = await SelectById(idEquipo);

            ViewBag.IdEquipo = idEquipo;
            ViewBag.Mensaje = "Se editara el equipo";
            ViewBag.EquipoDigitalizacion = dtoEquipoDigitalizacion;
            return View();
        }
        [HttpPost]
        [Route("insert")]
        public async Task Insert(string equipoDigitalizacion)
        {
            try
            {
                var dtoEquipoDigitalizacion = JsonConvert.DeserializeObject<EquipoDigitalizacionModel>(equipoDigitalizacion);
                EquipoDigitalizacion beequipoDigitalizacion;
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    beequipoDigitalizacion = _mapper.Map<EquipoDigitalizacion>(dtoEquipoDigitalizacion);
                    var content = new StringContent(JsonConvert.SerializeObject(beequipoDigitalizacion), System.Text.Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("api/equipodigitalizacion/insert", content);
                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception(result.StatusCode.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("select-by-id/{idEquipo}")]
        public async Task<EquipoDigitalizacionModel> SelectById(int idEquipo)
        {
            EquipoDigitalizacionModel dtoEquipoDigitalizacion;
            try
            {
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var result = await client.GetAsync("api/equipodigitalizacion/select-by-id/"+ idEquipo +"/");
                    if (result.IsSuccessStatusCode)
                    {
                        var equipoResult = result.Content.ReadAsStringAsync().Result;
                        var beEquipoDigitalizacion = JsonConvert.DeserializeObject<EquipoDigitalizacion>(equipoResult);
                        dtoEquipoDigitalizacion = _mapper.Map<EquipoDigitalizacionModel>(beEquipoDigitalizacion);
                    }
                    else
                    {
                        throw new Exception(result.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtoEquipoDigitalizacion;
        }
        [HttpPut]
        [Route("update/{idEquipoDigitalizacion}")]
        public async Task Update(int idEquipoDigitalizacion, string equipoDigitalizacion)
        {
            try
            {
                var dtoEquipoDigitalizacion = JsonConvert.DeserializeObject<EquipoDigitalizacion>(equipoDigitalizacion);
                EquipoDigitalizacion beEquipoDigitalizacion;
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    beEquipoDigitalizacion = _mapper.Map<EquipoDigitalizacion>(dtoEquipoDigitalizacion);
                    var content = new StringContent(JsonConvert.SerializeObject(beEquipoDigitalizacion), System.Text.Encoding.UTF8, "application/json");
                    var result = await client.PutAsync("api/equipodigitalizacion/update/" + idEquipoDigitalizacion, content);
                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception(result.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpDelete]
        [Route("delete/{idEquipoDigitalizacion}")]
        public async Task Delete(int idEquipoDigitalizacion)
        {
            using (var client = new HttpClient())
            {
                var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                client.BaseAddress = new Uri(digitalizacionApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.DeleteAsync("api/equipodigitalizacion/delete/" + idEquipoDigitalizacion + "/");
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception(result.StatusCode.ToString());
                }
            }
        }
        [HttpGet]
        [Route("pagination")]
        public async Task<IActionResult> Paginacion(string? texto, int pageSize, int currentPage, string orderBy, bool? sortOrder)
        {
            List<ExpandoObject> lstEquipos = new();
            try
            {
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string endpoint = $"api/equipodigitalizacion/pagination?texto={texto}&pageSize={pageSize}&currentPage={currentPage}&orderBy={orderBy}&sortOrder={sortOrder}";
                    var response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        lstEquipos = JsonConvert.DeserializeObject<List<ExpandoObject>>(result)!;
                    }
                    else
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(lstEquipos);
        }
    }
}

