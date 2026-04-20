using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaVirtualYanten.Data;
using TiendaVirtualYanten.Helpers;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Controllers
{
    public class RolController : Controller
    {
        private readonly TiendaContext _context;
        private readonly PermisoHelper _permisos;

        public RolController(TiendaContext context, PermisoHelper permisos)
        {
            _context = context;
            _permisos = permisos;
        }

        public IActionResult Index()
        {
            if (!_permisos.Tiene("Rol", "Ver"))
                return RedirectToAction("Denegado", "Home");

            var roles = _context.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            if (!_permisos.Tiene("Rol", "Crear"))
                return RedirectToAction("Denegado", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Rol rol)
        {
            if (!_permisos.Tiene("Rol", "Crear"))
                return RedirectToAction("Denegado", "Home");

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
            if (!_permisos.Tiene("Rol", "Editar"))
                return RedirectToAction("Denegado", "Home");

            var rol = _context.Roles.Find(id);
            if (rol == null) return RedirectToAction("Index");
            return View(rol);
        }

        [HttpPost]
        public IActionResult Edit(Rol rol)
        {
            if (!_permisos.Tiene("Rol", "Editar"))
                return RedirectToAction("Denegado", "Home");

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
            if (!_permisos.Tiene("Rol", "Eliminar"))
                return RedirectToAction("Denegado", "Home");

            var rol = _context.Roles.Find(id);
            if (rol == null) return RedirectToAction("Index");
            _context.Roles.Remove(rol);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ── PERMISOS ──────────────────────────────────────────────
        public IActionResult Permisos(int id)
        {
            if (!_permisos.Tiene("Rol", "Editar"))
                return RedirectToAction("Denegado", "Home");

            var rol = _context.Roles.Find(id);
            if (rol == null) return RedirectToAction("Index");

            var todosLosPermisos = _context.Permisos.ToList();
            var permisosDelRol = _context.RolPermisos
                .Where(rp => rp.RolId == id)
                .Select(rp => rp.PermisoId)
                .ToList();

            var modulos = todosLosPermisos.Select(p => p.Modulo).Distinct().OrderBy(m => m).ToList();
            var acciones = todosLosPermisos.Select(p => p.Accion).Distinct().OrderBy(a => a).ToList();

            var checkboxes = todosLosPermisos.Select(p => new PermisoCheckbox
            {
                PermisoId = p.Id,
                Modulo = p.Modulo,
                Accion = p.Accion,
                Asignado = permisosDelRol.Contains(p.Id)
            }).ToList();

            var vm = new AsignarPermisosViewModel
            {
                RolId = id,
                RolNombre = rol.Nombre,
                Modulos = modulos,
                Acciones = acciones,
                Permisos = checkboxes
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Permisos(int id, List<int> permisosSeleccionados)
        {
            if (!_permisos.Tiene("Rol", "Editar"))
                return RedirectToAction("Denegado", "Home");

            var anteriores = _context.RolPermisos.Where(rp => rp.RolId == id).ToList();
            _context.RolPermisos.RemoveRange(anteriores);

            if (permisosSeleccionados != null)
            {
                foreach (var permisoId in permisosSeleccionados)
                {
                    _context.RolPermisos.Add(new RolPermiso
                    {
                        RolId = id,
                        PermisoId = permisoId
                    });
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
