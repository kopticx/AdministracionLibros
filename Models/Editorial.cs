using AdministracionLibros.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdministracionLibros.Models
{
    public class Editorial
    {
        public int IdEditorial { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres")]
        [NombreMayusculas]
        [Remote(action: "VerificarExisteEditorial", controller: "Editoriales", AdditionalFields = nameof(IdEditorial))]
        public string Nombre { get; set; }
    }
}
