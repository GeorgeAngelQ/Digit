using Digitalizacion.LN;
using Microsoft.AspNetCore.Mvc;

namespace Digitalizacion.UI.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("")]
        [Route("login")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") != null)
            {
                return RedirectToAction("Index", "Home"); 
            }
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromForm] string usuario, [FromForm] string contrasenia)
        {
            var ln = new UsuarioSistemaLN();
            var usuarioValido = ln.Login(usuario, contrasenia);

            if (usuarioValido != null)
            {
                HttpContext.Session.SetInt32("IdUsuario", usuarioValido.IdUsuario);
                HttpContext.Session.SetString("Usuario", usuarioValido.Usuario);
                HttpContext.Session.SetString("Rol", usuarioValido.Rol);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Usuario o contraseña inválidos" });
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Login");
        }
    }
}
