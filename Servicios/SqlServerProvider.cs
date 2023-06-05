using AdministracionLibros.Helpers;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace AdministracionLibros.Servicios
{
    public class SqlServerProvider : ISqlServerProvider
    {
        private readonly string connectionString;
        private readonly string mySqlConnectionString;
        private readonly MySqlConHelper mySqlConHelper;

        public SqlServerProvider(IConfiguration configuration, MySqlConHelper mySqlConHelper)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            mySqlConnectionString = mySqlConHelper.GetConnectionString();
            this.mySqlConHelper = mySqlConHelper;
        }

        public IDbConnection GetDbConnection(int typeConnection)
        {
            switch (typeConnection)
            {
                case 1:
                    return new SqlConnection(connectionString);

                case 2:
                    return new MySqlConnection(mySqlConnectionString);

                default:
                    return null;
            }
        }
    }
}
