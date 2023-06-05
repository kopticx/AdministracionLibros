using AdministracionLibros.Models;
using Dapper;

namespace AdministracionLibros.Servicios
{
    public class RepositorioRoles : IRepositorioRol
    {
        private readonly ISqlServerProvider sqlServerProvider;

        public RepositorioRoles(ISqlServerProvider sqlServerProvider)
        {
            this.sqlServerProvider = sqlServerProvider;
        }

        public async Task<Rol> ObtenerRolPorNombre(string nombre)
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QueryFirstOrDefaultAsync<Rol>(@"SELECT * FROM Rol 
                                                             WHERE Nombre = @nombre", new { nombre });
        }

        public async Task<Rol> ObtenerRolPorId(int id)
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QueryFirstOrDefaultAsync<Rol>(@"SELECT * FROM Rol 
                                                             WHERE IdRol = @id", new { id });
        }
    }
}
