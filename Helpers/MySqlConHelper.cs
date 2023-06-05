using MySql.Data.MySqlClient;

namespace AdministracionLibros.Helpers
{
    public class MySqlConHelper
    {
        private readonly IConfiguration configuration;

        public MySqlConHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetConnectionString()
        {
            MySqlConnectionStringBuilder connectionString = new MySqlConnectionStringBuilder();
            connectionString.Server = configuration["DATABASE_URL:MYSQLHOST"];
            connectionString.UserID = configuration["DATABASE_URL:MYSQLUSER"];
            connectionString.Password = configuration["DATABASE_URL:MYSQLPASSWORD"];
            connectionString.Database = configuration["DATABASE_URL:MYSQLDATABASE"];
            connectionString.Port = uint.Parse(configuration["DATABASE_URL:MYSQLPORT"].ToString());

            return connectionString.ToString();
        }
    }
}
