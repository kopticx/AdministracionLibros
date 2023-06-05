using AdministracionLibros.Models;

namespace AdministracionLibros.Servicios
{
    public interface IRepositorioRol
    {
        Task<Rol> ObtenerRolPorId(int id);
        Task<Rol> ObtenerRolPorNombre(string nombre);
    }
}
