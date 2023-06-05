using AdministracionLibros.Models;
using AdministracionLibros.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdministracionLibros.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EditorialesController : Controller
    {
        private readonly IRepositorioEditoriales repositorioEditoriales;

        public EditorialesController(IRepositorioEditoriales repositorioEditoriales)
        {
            this.repositorioEditoriales = repositorioEditoriales;
        }

        public async Task<IActionResult> Index()
        {
            var modelo = await repositorioEditoriales.ObtenerEditoriales();

            return View(modelo);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Editorial model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await repositorioEditoriales.Crear(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Actualizar(int id)
        {
            var modelo = await repositorioEditoriales.ObtenerEditorialPorId(id);

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(Editorial editorial)
        {
            if (!ModelState.IsValid)
            {
                return View(editorial);
            }

            var existeEditorial = await repositorioEditoriales.ObtenerEditorialPorId(editorial.IdEditorial);

            if (existeEditorial is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioEditoriales.Actualizar(editorial);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteEditorial(string Nombre)
        {
            var existe = await repositorioEditoriales.Existe(Nombre);

            if (existe)
            {
                return Json($"La editorial {Nombre} ya existe");
            }

            return Json(true);
        }
    }
}
