using AdministracionLibros.Models;
using AdministracionLibros.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace AdministracionLibros.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IRepositorioReview repositorioReview;
        private readonly IRepositorioLibros repositorioLibros;
        private readonly IServicioUsuarios servicioUsuarios;

        public ReviewsController(IRepositorioReview repositorioReview, IRepositorioLibros repositorioLibros, 
                                 IServicioUsuarios servicioUsuarios)
        {
            this.repositorioReview = repositorioReview;
            this.repositorioLibros = repositorioLibros;
            this.servicioUsuarios = servicioUsuarios;
        }

        public async Task<IActionResult> Crear(int idLibro)
        {
            var existeLibro = await repositorioLibros.ObtenerLibroPorId(idLibro);

            if(existeLibro is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            var modelo = new ReviewViewModel
            {
                IdLibro = idLibro,
                IdUsuario = usuarioId,
                Libro = existeLibro
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }

            var existeLibro = await repositorioLibros.ObtenerLibroPorId(review.IdLibro);

            if (existeLibro is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            review.IdUsuario = servicioUsuarios.ObtenerUsuarioId();
            review.FechaReview = DateTime.Today;

            await repositorioReview.Crear(review);

            return RedirectToAction("Index", "Libros");
        }
    }
}
