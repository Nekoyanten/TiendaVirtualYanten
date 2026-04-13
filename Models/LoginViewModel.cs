using System.ComponentModel.DataAnnotations;

namespace TiendaVirtualYanten.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;
    }
}
