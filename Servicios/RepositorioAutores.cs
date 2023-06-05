using AdministracionLibros.Models;
using Dapper;

namespace AdministracionLibros.Servicios
{
    public class RepositorioAutores : IRepositorioAutores
    {
        private readonly ISqlServerProvider sqlServerProvider;

        public RepositorioAutores(ISqlServerProvider sqlServerProvider)
        {
            this.sqlServerProvider = sqlServerProvider;
        }

        public async Task Crear(Autor autor)
        {
            using var con = sqlServerProvider.GetDbConnection();

            await con.ExecuteAsync(@"INSERT INTO Autor(Nombre, FechaNacimiento, FechaMuerte, ImagenUrl, Biografia)
                                     VALUES (@Nombre, @FechaNacimiento, @FechaMuerte, @ImagenUrl, @Biografia)", autor);
        }

        public async Task Actualizar(Autor autor)
        {
            using var con = sqlServerProvider.GetDbConnection();

            await con.ExecuteAsync(@"UPDATE Autor SET Nombre = @Nombre, FechaNacimiento = @FechaNacimiento, FechaMuerte = @FechaMuerte, Biografia = @Biografia
                                     WHERE IdAutor = @IdAutor", autor);
        }

        public async Task<bool> Existe(string Nombre, int Id = 0)
        {
            using var con = sqlServerProvider.GetDbConnection();

            var existe = await con.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM Autor
                                                                   WHERE Nombre = @Nombre AND IdAutor <> @Id", new { Nombre, Id });

            return existe == 1;
        }

        public async Task<Autor> ObtenerAutorPorId(int id)
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QueryFirstOrDefaultAsync<Autor>(@"SELECT * FROM Autor
                                                               WHERE IdAutor = @id", new { id });
        }

        public async Task<IEnumerable<Autor>> ObtenerAutores()
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QueryAsync<Autor>(@"SELECT * FROM Autor");
        }
    }
}
