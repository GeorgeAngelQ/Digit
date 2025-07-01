using Digitalizacion.EN;
using Digitalizacion.DA;

namespace Digitalizacion.LN
{
    public class UsuarioSistemaLN
    {
        public void Insert(UsuarioSistema enUsuarioSistema)
        {
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
            usuarioSistemaDA.Update(idUsuario, enUsuarioSistema);
        }
        public void Delete(int idUsuario)
        {
            var usuarioSistemaDA = new UsuarioSistemaDA();
            usuarioSistemaDA.Delete(idUsuario);
        }
    }
}