using AdministracionLibros.Helpers;
using AdministracionLibros.Models;
using AdministracionLibros.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdministracionLibros.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class LibrosController : Controller
    {
        private readonly IRepositorioLibros repositorioLibros;
        private readonly IRepositorioAutores repositorioAutores;
        private readonly IRepositorioEditoriales repositorioEditoriales;
        private readonly IRepositorioReview repositorioReview;
        private readonly IServicioCloudinary servicioCloudinary;
        private readonly IMapper mapper;

        public LibrosController(IRepositorioLibros repositorioLibros, IRepositorioAutores repositorioAutores,
                                IRepositorioEditoriales repositorioEditoriales, IRepositorioReview repositorioReview,
                                IServicioCloudinary servicioCloudinary, IMapper mapper)
        {
            this.repositorioLibros = repositorioLibros;
            this.repositorioAutores = repositorioAutores;
            this.repositorioEditoriales = repositorioEditoriales;
            this.repositorioReview = repositorioReview;
            this.servicioCloudinary = servicioCloudinary;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var modelo = await repositorioLibros.ObtenerLibros();

            return View(modelo);
        }

        public async Task<IActionResult> Crear()
        {
            var modelo = new LibroCreacionViewModel()
            {
                Autores = await ObtenerAutores(),
                Editoriales = await ObtenerEditoriales()
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(LibroCreacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Autores = await ObtenerAutores();
                model.Editoriales = await ObtenerEditoriales();

                return View(model);
            }

            var autor = await repositorioAutores.ObtenerAutorPorId(model.IdAutor);

            if(autor is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var editorial = await repositorioEditoriales.ObtenerEditorialPorId(model.IdEditorial);

            if(editorial is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            model.ImagenUrl = await servicioCloudinary.UploadImageLibro(model.ImagenUrl);

            await repositorioLibros.Crear(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Actualizar(int id)
        {
            var model = await repositorioLibros.ObtenerLibroPorId(id);
            model.FechaPublicacion = FechasConvertHelper.FormatoFechaActualizaciones(model.FechaPublicacion);
            model.Autores = await ObtenerAutores();
            model.Editoriales = await ObtenerEditoriales();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(model is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(Libro libro)
        {
            var model = await repositorioLibros.ObtenerLibroPorId(libro.IdLibro);

            if (model is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            model.Autores = await ObtenerAutores();
            model.Editoriales = await ObtenerEditoriales();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await repositorioLibros.Actualizar(libro);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Detalles(int idLibro)
        {
            var existeLibro = await repositorioLibros.ObtenerLibroPorId(idLibro);

            if(existeLibro is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var modelo = mapper.Map<LibroDetallesViewModel>(existeLibro);
            modelo.Autor = await repositorioAutores.ObtenerAutorPorId(modelo.IdAutor);
            modelo.Editorial = await repositorioEditoriales.ObtenerEditorialPorId(modelo.IdEditorial);
            modelo.Reviews = await repositorioReview.ObtenerReviewsPorLibro(modelo.IdLibro);

            return View(modelo);
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerAutores()
        {
            var autores = await repositorioAutores.ObtenerAutores();
            var resultado = autores.Select(x => new SelectListItem(x.Nombre, x.IdAutor.ToString())).ToList();

            var opcionPorDefecto = new SelectListItem("-- Seleccione un Autor --", "0", true);

            resultado.Insert(0, opcionPorDefecto);

            return resultado;
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerEditoriales()
        {
            var editoriales = await repositorioEditoriales.ObtenerEditoriales();
            var resultado = editoriales.Select(x => new SelectListItem(x.Nombre, x.IdEditorial.ToString())).ToList();

            var opcionPorDefecto = new SelectListItem("-- Seleccione una Editorial --", "0", true);

            resultado.Insert(0, opcionPorDefecto);

            return resultado;
        }
    }
}
