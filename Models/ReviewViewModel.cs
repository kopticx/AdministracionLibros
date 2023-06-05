namespace AdministracionLibros.Models
{
    public class ReviewViewModel : Review
    {
        public Libro Libro { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
    }
}
