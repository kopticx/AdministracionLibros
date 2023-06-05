using AdministracionLibros.Models;
using Dapper;

namespace AdministracionLibros.Servicios
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly ISqlServerProvider sqlServerProvider;

        public RepositorioUsuarios(ISqlServerProvider sqlServerProvider)
        {
            this.sqlServerProvider = sqlServerProvider;
        }

        public async Task<IEnumerable<Usuario>> ListadoUsuarios()
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QueryAsync<Usuario>(@"SELECT * FROM Usuario");
        }

        public async Task CrearUsuario(Usuario usuario)
        {
            using var con = sqlServerProvider.GetDbConnection();

            await con.QueryAsync(@"INSERT INTO Usuario(UserName, Email, EmailNormalizado, Password, TipoUsuario) 
                                   VALUES (@UserName, @Email, @EmailNormalizado, @Password, 2)", usuario);
        }

        public async Task ActualizarImagen(int IdUsuario, string Url)
        {
            using var con = sqlServerProvider.GetDbConnection();

            await con.ExecuteAsync(@"UPDATE Usuario SET ImagenUrl = @Url
                                     WHERE IdUsuario = @IdUSuario", new { IdUsuario, Url });
        }

        public async Task<Usuario> BuscarUsuarioPorNombre(string UserName)
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QuerySingleOrDefaultAsync<Usuario>(@"SELECT * FROM Usuario
                                                                  WHERE UserName = @UserName", new { UserName });
        }

        public async Task<Usuario> BuscarUsuarioPorEmailNormalizado(string emailNormalizado)
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QuerySingleOrDefaultAsync<Usuario>(@"SELECT * FROM Usuario
                                                                  WHERE EmailNormalizado = @emailNormalizado", new { emailNormalizado });
        }

        public async Task<Usuario> BuscarUsuarioPorId(string id)
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QuerySingleOrDefaultAsync<Usuario>(@"SELECT * FROM Usuario
                                                                  WHERE IdUsuario = @id", new { id });
        }
    }
}
