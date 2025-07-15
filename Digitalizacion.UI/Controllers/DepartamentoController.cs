using System.Net.Http.Headers;
using System.Text;
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
    [Route("mantenimiento/departamento")]
    public class DepartamentoController : Controller
    {
        private IMapper _mapper;
        public DepartamentoController()
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
            ViewBag.Mensaje = "No hay departamentos";
            return View();
        }
        [HttpGet]
        [Route("nuevo")]
        public IActionResult Nuevo()
        {
            ViewBag.Mensaje = "Crear nuevo departamento";
            return View();
        }
        [HttpGet]
        [Route("editar/{idDepartamento}")]
        public async Task<IActionResult> Editar(int idDepartamento)
        {
            DepartamentoModel dtoDepartamento;
            dtoDepartamento = await SelectById(idDepartamento);
            ViewBag.IdDepartamento = idDepartamento;
            ViewBag.Mensaje = "Se editara el departamento";
            ViewBag.Departamento = dtoDepartamento;
            return View();
        }
        [HttpPost]
        [Route("insert")]
        public async Task Insert(string departamento)
        {
            try
            {
                var dtoDepartamento = JsonConvert.DeserializeObject<DepartamentoModel>(departamento);
                Departamento beDepartamento;

                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    beDepartamento = _mapper.Map<Departamento>(dtoDepartamento);
                    var content = new StringContent(JsonConvert.SerializeObject(beDepartamento), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("api/departamento/insert", content);
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
        [Route("select-by-id/{idDepartamento}")]
        public async Task<DepartamentoModel> SelectById(int idDepartamento)
        {
            DepartamentoModel dtoDepartamento;
            try
            {
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var result = await client.GetAsync($"api/departamento/select-by-id/" + idDepartamento + "/");
                    if (result.IsSuccessStatusCode)
                    {
                        var response = result.Content.ReadAsStringAsync().Result;
                        var beDepartamento = JsonConvert.DeserializeObject<DepartamentoModel>(response);
                        dtoDepartamento = _mapper.Map<DepartamentoModel>(beDepartamento);
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
            return dtoDepartamento;
        }
        [HttpPut]
        [Route("update/{idDepartamento}")]
        public async Task Update(int idDepartamento, string departamento)
        {
            try
            {
                var dtoDepartamento = JsonConvert.DeserializeObject<DepartamentoModel>(departamento);
                Departamento beDepartamento;
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    beDepartamento = _mapper.Map<Departamento>(dtoDepartamento);
                    var content = new StringContent(JsonConvert.SerializeObject(beDepartamento), Encoding.UTF8, "application/json");
                    var result = await client.PutAsync("api/departamento/update/" + idDepartamento, content);
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
        [Route("delete/{idDepartamento}")]
        public async Task Delete(int idDepartamento)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var result = await client.DeleteAsync("api/departamento/delete/" + idDepartamento + "/");
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
    }
}
