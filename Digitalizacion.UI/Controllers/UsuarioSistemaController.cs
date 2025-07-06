using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using Digitalizacion.EN;
using Digitalizacion.UI.Models;
using Libreria;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Digitalizacion.UI.Controllers
{
    [Route("mantenimiento/usuariosistema")]
    public class UsuarioSistemaController : Controller
    {
        private IMapper _mapper;
        public UsuarioSistemaController()
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
            ViewBag.Mensaje = "No hay usuarios del sistema";
            return View();
        }
        [HttpGet]
        [Route("nuevo")]
        public IActionResult Nuevo()
        {
            ViewBag.Mensaje = "Crear nuevo usuario del sistema";
            return View();
        }
        [HttpGet]
        [Route("editar/{idUsuario}")]
        public async Task<IActionResult> Editar(int idUsuario)
        {
            UsuarioSistemaModel dtoUsuarioSistema;

            dtoUsuarioSistema = await SelectById(idUsuario);

            ViewBag.IdUsuario = idUsuario;
            ViewBag.Mensaje = "Se editara el usuario";
            ViewBag.UsuarioSistema = dtoUsuarioSistema;
            return View();
        }
        [HttpPost]
        [Route("insert")]
        public async Task Insert(string usuarioSistema)
        {
            try
            {
                var dtoUsuarioSistema = JsonConvert.DeserializeObject<UsuarioSistemaModel>(usuarioSistema);
                UsuarioSistema beUsuarioSistema;

                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    beUsuarioSistema = _mapper.Map<UsuarioSistema>(dtoUsuarioSistema);
                    var content = new StringContent(JsonConvert.SerializeObject(beUsuarioSistema), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("api/usuariosistema/insert", content);
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
        [Route("select-by-id/{idUsuario}")]
        public async Task<UsuarioSistemaModel> SelectById(int idUsuario)
        {
            UsuarioSistemaModel dtoUsuarioSistema;
            try
            {
                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var result = await client.GetAsync("api/usuariosistema/select-by-id/" + idUsuario + "/");
                    if (result.IsSuccessStatusCode)
                    {
                        var usuarioResult = result.Content.ReadAsStringAsync().Result;
                        var beUsuarioSistema = JsonConvert.DeserializeObject<UsuarioSistema>(usuarioResult);
                        dtoUsuarioSistema = _mapper.Map<UsuarioSistemaModel>(beUsuarioSistema);
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
            return dtoUsuarioSistema;
        }

        [HttpPut]
        [Route("update/{idUsuario}")]
        public async Task Update(int idUsuario, string usuarioSistema)
        {
            try
            {
                var dtoUsuarioSistema = JsonConvert.DeserializeObject<UsuarioSistemaModel>(usuarioSistema);
                UsuarioSistema beUsuarioSistema;

                using (var client = new HttpClient())
                {
                    var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                    client.BaseAddress = new Uri(digitalizacionApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                    beUsuarioSistema = _mapper.Map<UsuarioSistema>(dtoUsuarioSistema);
                    var content = new StringContent(JsonConvert.SerializeObject(beUsuarioSistema), Encoding.UTF8, "application/json");
                    var result = await client.PutAsync("api/usuariosistema/update/" + idUsuario, content);
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
        [Route("delete/{idUsuario}")]
        public async Task Delete(int idUsuario)
        {
            using (var client = new HttpClient())
            {
                var digitalizacionApi = ConfigurationJson.GetAppSettings("DigitalizacionApi");
                client.BaseAddress = new Uri(digitalizacionApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.DeleteAsync("api/usuariosistema/delete/" + idUsuario + "/");
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception(result.StatusCode.ToString());
                }
            }
        }
    }
}
