using Microsoft.AspNetCore.Mvc;
using TiendaVirtualYanten.Data;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Controllers
{
    public class RolController : Controller
    {
        private readonly TiendaContext _context;

        public RolController(TiendaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Roles.Add(rol);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        public IActionResult Edit(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol == null) return RedirectToAction("Index");
            return View(rol);
        }

        [HttpPost]
        public IActionResult Edit(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Roles.Update(rol);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        public IActionResult Delete(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol == null) return RedirectToAction("Index");
            _context.Roles.Remove(rol);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
