using AdministracionLibros.Models;

namespace AdministracionLibros.Servicios
{
    public interface IRepositorioUsuarios
    {
        Task ActualizarImagen(int IdUsuario, string Url);
        Task<Usuario> BuscarUsuarioPorEmailNormalizado(string emailNormalizado);
        Task<Usuario> BuscarUsuarioPorId(string id);
        Task<Usuario> BuscarUsuarioPorNombre(string UserName);
        Task CrearUsuario(Usuario usuario);
        Task<IEnumerable<Usuario>> ListadoUsuarios();
    }
}
