using Microsoft.AspNetCore.Mvc;
using TiendaVirtualYanten.Data;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly TiendaContext _context;

        public CategoriaController(TiendaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categorias = _context.Categorias.ToList();
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
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
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return RedirectToAction("Index");
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
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
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return RedirectToAction("Index");
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
