using System.Text.Json;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Helpers
{
    public static class CarritoHelper
    {
        private const string KEY = "Carrito";

        public static CarritoViewModel Obtener(ISession session)
        {
            var json = session.GetString(KEY);
            if (string.IsNullOrEmpty(json))
                return new CarritoViewModel();
            return JsonSerializer.Deserialize<CarritoViewModel>(json) ?? new CarritoViewModel();
        }

        public static void Guardar(ISession session, CarritoViewModel carrito)
        {
            session.SetString(KEY, JsonSerializer.Serialize(carrito));
        }

        public static void Agregar(ISession session, CarritoItem item)
        {
            var carrito = Obtener(session);
            var existente = carrito.Items.FirstOrDefault(i => i.ProductoId == item.ProductoId);
            if (existente != null)
                existente.Cantidad += item.Cantidad;
            else
                carrito.Items.Add(item);
            Guardar(session, carrito);
        }

        public static void Quitar(ISession session, int productoId)
        {
            var carrito = Obtener(session);
            carrito.Items.RemoveAll(i => i.ProductoId == productoId);
            Guardar(session, carrito);
        }

        public static void Vaciar(ISession session)
        {
            session.Remove(KEY);
        }
    }
}
