using AdministracionLibros.Models;

namespace AdministracionLibros.Servicios
{
    public interface IRepositorioAutores
    {
        Task Actualizar(Autor autor);
        Task Crear(Autor autor);
        Task<bool> Existe(string Nombre, int Id = 0);
        Task<IEnumerable<Autor>> ObtenerAutores();
        Task<Autor> ObtenerAutorPorId(int id);
    }
}
