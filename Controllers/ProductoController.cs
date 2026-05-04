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
        private readonly IWebHostEnvironment _env;

        public ProductoController(TiendaContext context, PermisoHelper permisos, IWebHostEnvironment env)
        {
            _context = context;
            _permisos = permisos;
            _env = env;
        }

        // Vista tipo tienda con cards
        public IActionResult Tienda(string? buscar, int? categoriaId)
        {
            var query = _context.Productos.Include(p => p.Categoria).AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
                query = query.Where(p => p.Nombre.Contains(buscar) || p.Descripcion.Contains(buscar));

            if (categoriaId.HasValue)
                query = query.Where(p => p.CategoriaId == categoriaId);

            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Buscar = buscar;
            ViewBag.CategoriaId = categoriaId;
            ViewBag.CarritoCount = CarritoHelper.Obtener(HttpContext.Session).TotalItems;

            return View(query.ToList());
        }

        // Detalle de un producto
        public IActionResult Detalle(int id)
        {
            var producto = _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);

            if (producto == null) return RedirectToAction("Tienda");
            ViewBag.CarritoCount = CarritoHelper.Obtener(HttpContext.Session).TotalItems;
            return View(producto);
        }

        // CRUD admin
        public IActionResult Index()
        {
            if (!_permisos.Tiene("Producto", "Ver"))
                return RedirectToAction("Denegado", "Home");

            ViewBag.PuedeCrear = _permisos.Tiene("Producto", "Crear");
            ViewBag.PuedeEditar = _permisos.Tiene("Producto", "Editar");
            ViewBag.PuedeEliminar = _permisos.Tiene("Producto", "Eliminar");
            var productos = _context.Productos.Include(p => p.Categoria).ToList();
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
        public async Task<IActionResult> Create(Producto producto)
        {
            if (!_permisos.Tiene("Producto", "Crear"))
                return RedirectToAction("Denegado", "Home");

            if (ModelState.IsValid)
            {
                if (producto.ImagenFile != null && producto.ImagenFile.Length > 0)
                    producto.ImagenUrl = await GuardarImagen(producto.ImagenFile);

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
        public async Task<IActionResult> Edit(Producto producto)
        {
            if (!_permisos.Tiene("Producto", "Editar"))
                return RedirectToAction("Denegado", "Home");

            if (ModelState.IsValid)
            {
                if (producto.ImagenFile != null && producto.ImagenFile.Length > 0)
                {
                    // Borrar imagen anterior si existe
                    if (!string.IsNullOrEmpty(producto.ImagenUrl))
                        BorrarImagen(producto.ImagenUrl);
                    producto.ImagenUrl = await GuardarImagen(producto.ImagenFile);
                }
                else
                {
                    // Conservar imagen anterior
                    var imagenActual = _context.Productos.AsNoTracking()
                        .Where(p => p.Id == producto.Id)
                        .Select(p => p.ImagenUrl)
                        .FirstOrDefault();
                    producto.ImagenUrl = imagenActual;
                }

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

            if (!string.IsNullOrEmpty(producto.ImagenUrl))
                BorrarImagen(producto.ImagenUrl);

            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ── Helpers de imagen ──────────────────────────────────────
        private async Task<string> GuardarImagen(IFormFile file)
        {
            var carpeta = Path.Combine(_env.WebRootPath, "images", "productos");
            Directory.CreateDirectory(carpeta);
            var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var ruta = Path.Combine(carpeta, nombreArchivo);
            using var stream = new FileStream(ruta, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"/images/productos/{nombreArchivo}";
        }

        private void BorrarImagen(string imagenUrl)
        {
            var ruta = Path.Combine(_env.WebRootPath, imagenUrl.TrimStart('/'));
            if (System.IO.File.Exists(ruta))
                System.IO.File.Delete(ruta);
        }
    }
}
