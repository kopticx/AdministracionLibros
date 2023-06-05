using System.ComponentModel.DataAnnotations;

namespace AdministracionLibros.Models
{
    public class UsuarioLoginViewModel
    {
        [Display(Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 4, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres")]
        public string UserName { get; set; }
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }
        public bool Recuerdame { get; set; }
    }
}
