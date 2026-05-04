using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaVirtualYanten.Data;
using TiendaVirtualYanten.Helpers;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Controllers
{
    public class CarritoController : Controller
    {
        private readonly TiendaContext _context;

        public CarritoController(TiendaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var carrito = CarritoHelper.Obtener(HttpContext.Session);
            return View(carrito);
        }

        [HttpPost]
        public IActionResult Agregar(int productoId, int cantidad = 1)
        {
            var producto = _context.Productos.Find(productoId);
            if (producto == null) return RedirectToAction("Tienda", "Producto");

            if (cantidad < 1) cantidad = 1;
            if (cantidad > producto.Stock) cantidad = producto.Stock;

            CarritoHelper.Agregar(HttpContext.Session, new CarritoItem
            {
                ProductoId = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Cantidad = cantidad,
                ImagenUrl = producto.ImagenUrl
            });

            TempData["Mensaje"] = $"'{producto.Nombre}' agregado al carrito.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Quitar(int productoId)
        {
            CarritoHelper.Quitar(HttpContext.Session, productoId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Vaciar()
        {
            CarritoHelper.Vaciar(HttpContext.Session);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ActualizarCantidad(int productoId, int cantidad)
        {
            var carrito = CarritoHelper.Obtener(HttpContext.Session);
            var item = carrito.Items.FirstOrDefault(i => i.ProductoId == productoId);
            if (item != null)
            {
                if (cantidad <= 0)
                    carrito.Items.Remove(item);
                else
                    item.Cantidad = cantidad;
                CarritoHelper.Guardar(HttpContext.Session, carrito);
            }
            return RedirectToAction("Index");
        }
    }
}
