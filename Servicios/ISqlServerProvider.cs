using System.Data;

namespace AdministracionLibros.Servicios
{
    public interface ISqlServerProvider
    {
        //1 SQl
        //2 MySQL
        IDbConnection GetDbConnection(int typeConnection = 1);
    }
}
