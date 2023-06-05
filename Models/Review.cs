using System.ComponentModel.DataAnnotations;

namespace AdministracionLibros.Models
{
    public class Review
    {
        public int IdReview { get; set; }
        public int IdLibro { get; set; }
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:5000, MinimumLength = 15, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres")]
        public string Opinion { get; set; }
        public DateTime FechaReview { get; set; }
    }
}
