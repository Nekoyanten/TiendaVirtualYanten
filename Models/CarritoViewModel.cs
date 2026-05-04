namespace TiendaVirtualYanten.Models
{
    public class CarritoViewModel
    {
        public List<CarritoItem> Items { get; set; } = new();
        public double Total => Items.Sum(i => i.Subtotal);
        public int TotalItems => Items.Sum(i => i.Cantidad);
    }
}
