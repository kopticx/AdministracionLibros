using AdministracionLibros.Helpers;
using AdministracionLibros.Models;
using AdministracionLibros.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AdministracionLibros.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AutoresController : Controller
    {
        private readonly IRepositorioAutores repositorioAutores;
        private readonly IServicioCloudinary servicioCloudinary;

        public AutoresController(IRepositorioAutores repositorioAutores, IServicioCloudinary servicioCloudinary)
        {
            this.repositorioAutores = repositorioAutores;
            this.servicioCloudinary = servicioCloudinary;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var autores = await repositorioAutores.ObtenerAutores();

            return View(autores);
        }

        public IActionResult Crear()
        {
            var autor = new Autor();

            return View(autor);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            autor.ImagenUrl = await servicioCloudinary.UploadImageAutor(autor.ImagenUrl);

            await repositorioAutores.Crear(autor);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Actualizar(int id)
        {
            var modelo = await repositorioAutores.ObtenerAutorPorId(id);
            modelo.FechaNacimiento = FechasConvertHelper.FormatoFechaActualizaciones(modelo.FechaNacimiento);
            modelo.FechaMuerte = FechasConvertHelper.FormatoFechaActualizaciones(modelo.FechaMuerte);

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(Autor modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var autor = await repositorioAutores.ObtenerAutorPorId(modelo.IdAutor);

            if(autor is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioAutores.Actualizar(modelo);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Detalles(int idAutor)
        {
            var autor = await repositorioAutores.ObtenerAutorPorId(idAutor);

            if (autor is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(autor);
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteAutor(string Nombre, int IdAutor)
        {
            var existe = await repositorioAutores.Existe(Nombre, IdAutor);

            if (existe)
            {
                return Json($"El Autor {Nombre} ya existe");
            }

            return Json(true);
        }
    }
}
