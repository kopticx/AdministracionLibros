using AdministracionLibros.Models;

namespace AdministracionLibros.Servicios
{
    public interface IRepositorioLibros
    {
        Task Actualizar(Libro libro);
        Task Crear(Libro libro);
        Task<LibroCreacionViewModel> ObtenerLibroPorId(int id);
        Task<IEnumerable<ListadoLibros>> ObtenerLibros();
    }
}
