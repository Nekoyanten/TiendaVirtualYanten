using System.ComponentModel.DataAnnotations;

namespace TiendaVirtualYanten.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // VALIDACIÓN 2: Correo obligatorio y con formato válido
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        public string Correo { get; set; }
        public string Rol { get; set; }

        [Required]
        [RegularExpression(@"^3\d{9}$",
            ErrorMessage = "El celular debe estar entre 3000000000 y 3999999999")]
        public string celular { get; set; }
    }
}