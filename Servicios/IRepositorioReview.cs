using AdministracionLibros.Models;

namespace AdministracionLibros.Servicios
{
    public interface IRepositorioReview
    {
        Task Crear(Review review);
        Task<IEnumerable<ReviewViewModel>> ObtenerReviewsPorLibro(int idLibro);
    }
}
