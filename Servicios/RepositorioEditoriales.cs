using AdministracionLibros.Models;
using Dapper;

namespace AdministracionLibros.Servicios
{
    public class RepositorioEditoriales : IRepositorioEditoriales
    {
        private readonly ISqlServerProvider sqlServerProvider;

        public RepositorioEditoriales(ISqlServerProvider sqlServerProvider)
        {
            this.sqlServerProvider = sqlServerProvider;
        }

        public async Task Crear(Editorial editorial)
        {
            using var con = sqlServerProvider.GetDbConnection();

            await con.ExecuteAsync(@"INSERT INTO Editorial(Nombre)
                               VALUES (@Nombre)", editorial);
        }

        public async Task Actualizar(Editorial editorial)
        {
            using var con = sqlServerProvider.GetDbConnection();

            await con.ExecuteAsync(@"UPDATE Editorial SET Nombre = @Nombre
                                WHERE IdEditorial = @IdEditorial", editorial);
        }

        public async Task<bool> Existe(string Nombre, int id = 0)
        {
            using var con = sqlServerProvider.GetDbConnection();

            var existe = await con.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM Editorial
                                                             WHERE Nombre = @Nombre AND IdEditorial <> @id", new { Nombre, id });

            return existe == 1;
        }

        public async Task<Editorial> ObtenerEditorialPorId(int id)
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QueryFirstOrDefaultAsync<Editorial>(@"SELECT * FROM Editorial
                                                            WHERE IdEditorial = @id", new { id });
        }

        public async Task<IEnumerable<Editorial>> ObtenerEditoriales()
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QueryAsync<Editorial>(@"SELECT * FROM Editorial");
        }
    }
}
