using System.ComponentModel.DataAnnotations;

namespace TiendaVirtualYanten.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de celular es obligatorio")]
        [RegularExpression(@"^3\d{9}$",
            ErrorMessage = "El celular debe tener 10 dígitos y empezar por 3")]
        [Display(Name = "Número de celular")]
        public string Celular { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 6,
            ErrorMessage = "La contraseña debe tener mínimo 6 caracteres")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un rol")]
        [Display(Name = "Rol")]
        public int RolId { get; set; }

        // Navegación
        public Rol? Rol { get; set; }
    }
}
