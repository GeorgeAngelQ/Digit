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
    [Route("mantenimiento/responsable")]
    public class ResponsableController : Controller
    {
        private IMapper _mapper;
        public ResponsableController()
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
            ViewBag.Mensaje = "No hay responsables registrados";
            return View();
        }
        [HttpGet]
        [Route("nuevo")]
        public IActionResult Nuevo()
        {
            ViewBag.Mensaje = "Crear nuevo responsable";
            return View();
        }
        [HttpGet]
        [Route("editar/{idResponsable}")]
        public async Task<IActionResult> Editar(int idResponsable)
        {
            ResponsableModel dtoResponsable;
            dtoResponsable = await SelectById(idResponsable);

            ViewBag.IdResponsable = idResponsable;
            ViewBag.Mensaje = "Se editara el responsable";
            ViewBag.Responsable = dtoResponsable;
            return View();
        }
        [HttpPost]
        [Route("insert")]
        public async Task Insert(string responsable)
        {
            try
            {
                var dtoResponsable = JsonConvert.DeserializeObject<ResponsableModel>(responsable);
                Responsable beResponsable;
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    beResponsable = _mapper.Map<Responsable>(dtoResponsable);
                    var content = new StringContent(JsonConvert.SerializeObject(beResponsable), System.Text.Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("api/responsable/insert", content);
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
        [Route("select-by-id/{idResponsable}")]
        public async Task<ResponsableModel> SelectById(int idResponsable)
        {
            ResponsableModel dtoResponsable;
            try
            {
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var result = await client.GetAsync("api/responsable/select-by-id/" + idResponsable + "/");
                    if (result.IsSuccessStatusCode)
                    {
                        var responsableResult = result.Content.ReadAsStringAsync().Result;
                        var beResponsable = JsonConvert.DeserializeObject<Responsable>(responsableResult);
                        dtoResponsable = _mapper.Map<ResponsableModel>(beResponsable);
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

            return dtoResponsable;
        }
        [HttpPut]
        [Route("update/{idResponsable}")]
        public async Task Update(int idResponsable, string responsable)
        {
            try
            {
                var dtoResponsable = JsonConvert.DeserializeObject<ResponsableModel>(responsable);
                Responsable beResponsable;
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    beResponsable = _mapper.Map<Responsable>(dtoResponsable);
                    var content = new StringContent(JsonConvert.SerializeObject(beResponsable), System.Text.Encoding.UTF8, "application/json");
                    var result = await client.PutAsync("api/responsable/update/" + idResponsable, content);
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
        [Route("delete/{idResponsable}")]
        public async Task Delete(int idResponsable)
        {
            using (var client = new HttpClient())
            {
                var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                client.BaseAddress = new Uri(digitalizacionApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.DeleteAsync("api/responsable/delete/" + idResponsable + "/");
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception(result.StatusCode.ToString());
                }
            }
        }
    }
}
