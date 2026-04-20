using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaVirtualYanten.Data;
using TiendaVirtualYanten.Helpers;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Controllers
{
    public class ProductoController : Controller
    {
        private readonly TiendaContext _context;
        private readonly PermisoHelper _permisos;

        public ProductoController(TiendaContext context, PermisoHelper permisos)
        {
            _context = context;
            _permisos = permisos;
        }

        public IActionResult Index()
        {
            if (!_permisos.Tiene("Producto", "Ver"))
                return RedirectToAction("Denegado", "Home");

            ViewBag.PuedeCrear = _permisos.Tiene("Producto", "Crear");
            ViewBag.PuedeEditar = _permisos.Tiene("Producto", "Editar");
            ViewBag.PuedeEliminar = _permisos.Tiene("Producto", "Eliminar");
            var productos = _context.Productos
                .Include(p => p.Categoria)
                .ToList();
            return View(productos);
        }

        public IActionResult Create()
        {
            if (!_permisos.Tiene("Producto", "Crear"))
                return RedirectToAction("Denegado", "Home");

            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (!_permisos.Tiene("Producto", "Crear"))
                return RedirectToAction("Denegado", "Home");

            if (ModelState.IsValid)
            {
                _context.Productos.Add(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(producto);
        }

        public IActionResult Edit(int id)
        {
            if (!_permisos.Tiene("Producto", "Editar"))
                return RedirectToAction("Denegado", "Home");

            var producto = _context.Productos.Find(id);
            if (producto == null) return RedirectToAction("Index");
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(producto);
        }

        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            if (!_permisos.Tiene("Producto", "Editar"))
                return RedirectToAction("Denegado", "Home");

            if (ModelState.IsValid)
            {
                _context.Productos.Update(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(producto);
        }

        public IActionResult Delete(int id)
        {
            if (!_permisos.Tiene("Producto", "Eliminar"))
                return RedirectToAction("Denegado", "Home");

            var producto = _context.Productos.Find(id);
            if (producto == null) return RedirectToAction("Index");
            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
