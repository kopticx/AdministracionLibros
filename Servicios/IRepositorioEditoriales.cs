using AdministracionLibros.Models;

namespace AdministracionLibros.Servicios
{
    public interface IRepositorioEditoriales
    {
        Task Actualizar(Editorial editorial);
        Task Crear(Editorial editorial);
        Task<bool> Existe(string Nombre, int id = 0);
        Task<IEnumerable<Editorial>> ObtenerEditoriales();
        Task<Editorial> ObtenerEditorialPorId(int id);
    }
}
