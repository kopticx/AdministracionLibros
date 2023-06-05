using AdministracionLibros.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdministracionLibros.Models
{
    public class Autor
    {
        public int IdAutor { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "El campo {0} deber tener entre {2} y {1} caracteres")]
        [NombreMayusculas]
        [Remote(action: "VerificarExisteAutor", controller: "Autores", AdditionalFields = nameof(IdAutor))]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }
        [Display(Name = "Fecha de Muerte")]
        public string FechaMuerte { get; set; }
        [Required(ErrorMessage = "El campo de imagen es requerido")]
        public string ImagenUrl { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:5000, MinimumLength = 50, ErrorMessage = "El campo {0} deber tener entre {2} y {1} caracteres")]
        public string Biografia { get; set; }
    }
}
