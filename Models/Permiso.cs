using System.ComponentModel.DataAnnotations;

namespace TiendaVirtualYanten.Models
{
    public class Permiso
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Modulo { get; set; } = string.Empty; // Producto, Categoria, Usuario, Rol

        [Required]
        [StringLength(20)]
        public string Accion { get; set; } = string.Empty; // Ver, Crear, Editar, Eliminar

        // Navegación
        public List<RolPermiso> RolPermisos { get; set; } = new();
    }
}
