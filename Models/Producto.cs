using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaVirtualYanten.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a 0")]
        [Display(Name = "Precio")]
        public double Precio { get; set; }

        [Range(0, 1000, ErrorMessage = "El stock debe estar entre 0 y 1000")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [StringLength(500)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty;

        [Display(Name = "Imagen")]
        public string? ImagenUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        [NotMapped]
        public IFormFile? ImagenFile { get; set; }

        public double CalcularValorInventario() => Precio * Stock;
        public bool TieneStock() => Stock > 0;
    }
}
