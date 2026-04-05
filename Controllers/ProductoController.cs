using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaVirtualYanten.Data;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Controllers
{
    public class ProductoController : Controller
    {
        private readonly TiendaContext _context;

        public ProductoController(TiendaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Productos
                .Include(p => p.Categoria)
                .ToList();
            return View(productos);
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
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
            var producto = _context.Productos.Find(id);
            if (producto == null) return RedirectToAction("Index");
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(producto);
        }

        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
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
            var producto = _context.Productos.Find(id);
            if (producto == null) return RedirectToAction("Index");
            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
