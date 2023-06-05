using AdministracionLibros.Models;
using Dapper;

namespace AdministracionLibros.Servicios
{
    public class RepositorioReview : IRepositorioReview
    {
        private readonly ISqlServerProvider sqlServerProvider;

        public RepositorioReview(ISqlServerProvider sqlServerProvider)
        {
            this.sqlServerProvider = sqlServerProvider;
        }

        public async Task Crear(Review review)
        {
            using var con = sqlServerProvider.GetDbConnection();

            await con.ExecuteAsync(@"INSERT INTO Review(IdLibro, IdUsuario, Opinion, FechaReview) 
                                            VALUES (@IdLibro, @IdUsuario, @Opinion, @FechaReview)", review);
        }

        public async Task<IEnumerable<ReviewViewModel>> ObtenerReviewsPorLibro(int idLibro)
        {
            using var con = sqlServerProvider.GetDbConnection();

            return await con.QueryAsync<ReviewViewModel>(@"SELECT R.*, U.UserName AS UserName, U.ImagenUrl AS UserImage
                                                           FROM Review R
                                                           INNER JOIN Libro L
                                                           ON L.IdLibro = R.IdLibro
                                                           INNER JOIN Usuario U
                                                           ON U.IdUsuario = R.IdUsuario
                                                           WHERE L.IdLibro = @idLibro", new { idLibro });
        }
    }
}
