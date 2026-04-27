using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaVirtualYanten.Data;
using TiendaVirtualYanten.Helpers;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Controllers
{
    public class LoginController : Controller
    {
        private readonly TiendaContext _context;

        public LoginController(TiendaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioNombre") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var passwordHash = HashHelper.GetSha256(model.Password);

            var usuario = _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.Correo == model.Correo && u.Password == passwordHash);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos");
                return View(model);
            }

            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            HttpContext.Session.SetString("UsuarioRol", usuario.Rol?.Nombre ?? "");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
