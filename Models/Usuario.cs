using System.ComponentModel.DataAnnotations;

namespace AdministracionLibros.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Display(Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 4, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "Ingresa una direccion de correo valida")]
        public string Email { get; set; }
        public string EmailNormalizado { get; set; }
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }
        public int TipoUsuario { get; set; }
        [Required(ErrorMessage = "El campo de Imagen es requerido")]
        public string ImagenUrl { get; set; }
    }
}
