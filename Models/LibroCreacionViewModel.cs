using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdministracionLibros.Models
{
    public class LibroCreacionViewModel : Libro
    {
        public IEnumerable<SelectListItem> Autores { get; set; }
        public IEnumerable<SelectListItem> Editoriales { get; set; }
    }
}
