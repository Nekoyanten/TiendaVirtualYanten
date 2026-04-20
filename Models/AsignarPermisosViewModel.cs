namespace TiendaVirtualYanten.Models
{
    public class PermisoCheckbox
    {
        public int PermisoId { get; set; }
        public string Modulo { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public bool Asignado { get; set; }
    }

    public class AsignarPermisosViewModel
    {
        public int RolId { get; set; }
        public string RolNombre { get; set; } = string.Empty;
        public List<string> Modulos { get; set; } = new();
        public List<string> Acciones { get; set; } = new();
        public List<PermisoCheckbox> Permisos { get; set; } = new();
    }
}
