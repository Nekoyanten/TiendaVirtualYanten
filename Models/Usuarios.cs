using System.ComponentModel.DataAnnotations;

namespace TiendaVirtualYanten.Models
{
    public class Usuarios
    {
        public int Id { get; set; }

        // VALIDACIÓN 1: Nombre obligatorio
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; }

        // VALIDACIÓN 2: Correo obligatorio y con formato válido
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        // VALIDACIÓN 3: Celular con números válidos
        [Required(ErrorMessage = "El número de celular es obligatorio")]
        [RegularExpression(@"^3\d{9}$",
            ErrorMessage = "El celular debe tener 10 dígitos y no puede comenzar con 0")]
        [Display(Name = "Número de celular")]
        public string Celular { get; set; }

        // Campo Rol existente
        [Required(ErrorMessage = "El rol es obligatorio")]
        [StringLength(20)]
        [Display(Name = "Rol")]
        public string Rol { get; set; }
    }
}
