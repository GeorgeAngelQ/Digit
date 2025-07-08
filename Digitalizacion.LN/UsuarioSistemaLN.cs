using Digitalizacion.EN;
using Digitalizacion.DA;
using Digitalizacion.SEC;

namespace Digitalizacion.LN
{
    public class UsuarioSistemaLN
    {
        public void Insert(UsuarioSistema enUsuarioSistema)
        {
            if (!string.IsNullOrWhiteSpace(enUsuarioSistema.Contrasenia))
            {
                enUsuarioSistema.Contrasenia = PasswordHasher.HashPassword(enUsuarioSistema.Contrasenia);
            }
            var usuarioSistemaDA = new UsuarioSistemaDA();
            usuarioSistemaDA.Insert(enUsuarioSistema);
        }
        public UsuarioSistema SelectById(int idUsuario)
        {
            var usuarioSistemaDA = new UsuarioSistemaDA();
            UsuarioSistema enUsuarioSistema;
            enUsuarioSistema = usuarioSistemaDA.SelectById(idUsuario);
            return enUsuarioSistema;
        }
        public void Update(int idUsuario, UsuarioSistema enUsuarioSistema)
        {
            var usuarioSistemaDA = new UsuarioSistemaDA();

            if (!string.IsNullOrWhiteSpace(enUsuarioSistema.Contrasenia))
            {
                enUsuarioSistema.Contrasenia = PasswordHasher.HashPassword(enUsuarioSistema.Contrasenia);
            }
            else
            {
                var usuarioActual = usuarioSistemaDA.SelectById(idUsuario);
                enUsuarioSistema.Contrasenia = usuarioActual.Contrasenia;
            }

            usuarioSistemaDA.Update(idUsuario, enUsuarioSistema);
        }

        public void Delete(int idUsuario)
        {
            var usuarioSistemaDA = new UsuarioSistemaDA();
            usuarioSistemaDA.Delete(idUsuario);
        }
        public UsuarioSistema? Login(string usuario, string contrasenia)
        {
            var usuarioSistemaDA = new UsuarioSistemaDA();
            return usuarioSistemaDA.Login(usuario, contrasenia);
        }
        public List<UsuarioSistema> List()
        {
            var usuarioSistemaDA = new UsuarioSistemaDA();
            return usuarioSistemaDA.List();
        }

    }
}