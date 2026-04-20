using Microsoft.AspNetCore.Mvc;
using TiendaVirtualYanten.Data;
using TiendaVirtualYanten.Helpers;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly TiendaContext _context;
        private readonly PermisoHelper _permisos;

        public CategoriaController(TiendaContext context, PermisoHelper permisos)
        {
            _context = context;
            _permisos = permisos;
        }

        public IActionResult Index()
        {
            if (!_permisos.Tiene("Categoria", "Ver"))
                return RedirectToAction("Denegado", "Home");

            ViewBag.PuedeCrear = _permisos.Tiene("Categoria", "Crear");
            ViewBag.PuedeEditar = _permisos.Tiene("Categoria", "Editar");
            ViewBag.PuedeEliminar = _permisos.Tiene("Categoria", "Eliminar");
            var categorias = _context.Categorias.ToList();
            return View(categorias);
        }

        public IActionResult Create()
        {
            if (!_permisos.Tiene("Categoria", "Crear"))
                return RedirectToAction("Denegado", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (!_permisos.Tiene("Categoria", "Crear"))
                return RedirectToAction("Denegado", "Home");

            if (ModelState.IsValid)
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        public IActionResult Edit(int id)
        {
            if (!_permisos.Tiene("Categoria", "Editar"))
                return RedirectToAction("Denegado", "Home");

            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return RedirectToAction("Index");
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            if (!_permisos.Tiene("Categoria", "Editar"))
                return RedirectToAction("Denegado", "Home");

            if (ModelState.IsValid)
            {
                _context.Categorias.Update(categoria);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        public IActionResult Delete(int id)
        {
            if (!_permisos.Tiene("Categoria", "Eliminar"))
                return RedirectToAction("Denegado", "Home");

            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return RedirectToAction("Index");
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
