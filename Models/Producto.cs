using System.ComponentModel.DataAnnotations;

namespace TiendaVirtualYanten.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a 0")]
        public double Precio { get; set; }

        [Range(0, 1000, ErrorMessage = "El stock debe estar entre 0 y 100")]
        public int Stock { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        public double CalcularValorInventario() => Precio * Stock;

        public bool TieneStock() => Stock > 0;
    }
}
