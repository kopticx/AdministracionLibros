using AdministracionLibros.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace AdministracionLibros.Models
{
    public class Libro
    {
        public int IdLibro { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "El campo {0} deber tener entre {2} y {1} caracteres")]
        [NombreMayusculas]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha de Publicacion")]
        [DataType(DataType.Date)]
        public string FechaPublicacion { get; set; }
        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar un Autor")]
        [Display(Name = "Autor")]
        public int IdAutor { get; set; }
        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar una Editorial")]
        [Display(Name = "Editorial")]
        public int IdEditorial { get; set; }
        [Display(Name = "Imagen de Libro")]
        [Required(ErrorMessage = "El campo de Imagen es requerido")]
        public string ImagenUrl { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 5000, MinimumLength = 50, ErrorMessage = "El campo {0} deber tener entre {2} y {1} caracteres")]
        public string Descripcion { get; set; }
    }
}
