using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaVirtualYanten.Data;
using TiendaVirtualYanten.Helpers;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly TiendaContext _context;
        private readonly PermisoHelper _permisos;

        public UsuarioController(TiendaContext context, PermisoHelper permisos)
        {
            _context = context;
            _permisos = permisos;
        }

        public IActionResult Index()
        {
            if (!_permisos.Tiene("Usuario", "Ver"))
                return RedirectToAction("Denegado", "Home");

            ViewBag.PuedeCrear = _permisos.Tiene("Usuario", "Crear");
            ViewBag.PuedeEditar = _permisos.Tiene("Usuario", "Editar");
            ViewBag.PuedeEliminar = _permisos.Tiene("Usuario", "Eliminar");
            var usuarios = _context.Usuarios
                .Include(u => u.Rol)
                .ToList();
            return View(usuarios);
        }

        public IActionResult Create()
        {
            if (!_permisos.Tiene("Usuario", "Crear"))
                return RedirectToAction("Denegado", "Home");

            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            if (!_permisos.Tiene("Usuario", "Crear"))
                return RedirectToAction("Denegado", "Home");

            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        public IActionResult Edit(int id)
        {
            if (!_permisos.Tiene("Usuario", "Editar"))
                return RedirectToAction("Denegado", "Home");

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return RedirectToAction("Index");
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Edit(Usuario usuario)
        {
            if (!_permisos.Tiene("Usuario", "Editar"))
                return RedirectToAction("Denegado", "Home");

            if (ModelState.IsValid)
            {
                _context.Entry(usuario).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        public IActionResult Delete(int id)
        {
            if (!_permisos.Tiene("Usuario", "Eliminar"))
                return RedirectToAction("Denegado", "Home");

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return RedirectToAction("Index");
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
