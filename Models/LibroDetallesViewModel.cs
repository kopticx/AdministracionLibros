namespace AdministracionLibros.Models
{
    public class LibroDetallesViewModel : Libro
    {
        public Autor Autor { get; set; }
        public Editorial Editorial { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
    }
}
