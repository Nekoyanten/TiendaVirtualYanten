using Microsoft.AspNetCore.Mvc;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            var categorias = new List<Categoria>
            {
                new Categoria { Id = 1, Nombre = "Electricos", Descripcion = "Electrodomesticos" },
                new Categoria { Id = 2, Nombre = "Deportes", Descripcion = "Protecciones, Balones y mas" },
                new Categoria { Id = 3, Nombre = "", Descripcion = "Sin nombre definido" },
            };
            return View(categorias);
        }
    }
}