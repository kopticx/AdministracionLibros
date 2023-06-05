using AdministracionLibros.Models;
using AdministracionLibros.Servicios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdministracionLibros.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IServicioCloudinary servicioCloudinary;
        private readonly IRepositorioUsuarios repositorioUsuarios;

        public UsuariosController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
                                  IServicioCloudinary servicioCloudinary, IRepositorioUsuarios repositorioUsuarios)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.servicioCloudinary = servicioCloudinary;
            this.repositorioUsuarios = repositorioUsuarios;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginViewModel modelo)
        {
            if(!ModelState.IsValid)
            {
                return View(modelo);
            }

            var resultado = await signInManager.PasswordSignInAsync(modelo.UserName, modelo.Password, modelo.Recuerdame, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Libros");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos");
                return View(modelo);
            }
        }

        public IActionResult SignIn()
        {
            var modelo = new Usuario();

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Usuario modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            var usuario = new Usuario() { Email = modelo.Email, UserName = modelo.UserName, TipoUsuario = 2 };
            var resultado = await userManager.CreateAsync(usuario, password: modelo.Password);

            if (resultado.Succeeded)
            {
                usuario = await repositorioUsuarios.BuscarUsuarioPorNombre(usuario.UserName);
                var imagenUrl = await servicioCloudinary.UploadImageUser(modelo.ImagenUrl);
                await repositorioUsuarios.ActualizarImagen(usuario.IdUsuario, imagenUrl); 

                await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Libros");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(modelo);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Libros");
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> ListadoUsuarios()
        {
            var listado = await repositorioUsuarios.ListadoUsuarios();

            return View(listado);
        }
    }
}
