using AdministracionLibros.Models;
using Dapper;

namespace AdministracionLibros.Servicios
{
    public class RepositorioLibros : IRepositorioLibros
    {
        private readonly ISqlServerProvider sqlServerProvider;

        public RepositorioLibros(ISqlServerProvider sqlServerProvider)
        {
            this.sqlServerProvider = sqlServerProvider;
        }

        public async Task Crear(Libro libro)
        {
            using var con = sqlServerProvider.GetDbConnection();

            await con.ExecuteAsync(@"INSERT INTO Libro(Nombre, FechaPublicacion, IdAutor, IdEditorial, ImagenUrl, Descripcion) 
                                     VALUES (@Nombre, @FechaPublicacion, @IdAutor, @IdEditorial, @ImagenUrl, @Descripcion)", libro);
        }

        public async Task Actualizar(Libro libro)
        {
            using var con = sqlServerProvider.GetDbConnection();

            await con.ExecuteAsync(@"UPDATE Libro SET Nombre = @Nombre, FechaPublicacion = @FechaPublicacion, IdAutor = @IdAutor, IdEditorial = @IdEditorial, 
                                                                ImagenUrl = @ImagenUrl, Descripcion = @Descripcion
                                                                WHERE IdLibro = @IdLibro", libro);
        }

        public async Task<IEnumerable<ListadoLibros>> ObtenerLibros()
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QueryAsync<ListadoLibros>(@"SELECT L.IdLibro, L.Nombre AS NombreLibro, A.Nombre AS NombreAutor, E.Nombre AS NombreEditorial
                                                         FROM Libro L
                                                         INNER JOIN Autor A
                                                         ON A.IdAutor = L.IdAutor
                                                         INNER JOIN Editorial E
                                                         ON E.IdEditorial = L.IdEditorial
                                                         ORDER BY L.Nombre");
        }

        public async Task<LibroCreacionViewModel> ObtenerLibroPorId(int id)
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QueryFirstOrDefaultAsync<LibroCreacionViewModel>(@"SELECT * FROM Libro
                                                                                WHERE IdLibro = @id", new { id });
        }
    }
}
