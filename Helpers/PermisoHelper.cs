using TiendaVirtualYanten.Data;

namespace TiendaVirtualYanten.Helpers
{
    public class PermisoHelper
    {
        private readonly TiendaContext _context;
        private readonly IHttpContextAccessor _httpContext;

        // Nombre del rol que siempre tiene acceso total
        private const string ROL_ADMIN = "Admin";

        public PermisoHelper(TiendaContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public bool Tiene(string modulo, string accion)
        {
            var rolNombre = _httpContext.HttpContext?.Session.GetString("UsuarioRol");
            if (string.IsNullOrEmpty(rolNombre)) return false;

            // El Admin siempre tiene acceso total
            if (rolNombre == ROL_ADMIN) return true;

            var rol = _context.Roles.FirstOrDefault(r => r.Nombre == rolNombre);
            if (rol == null) return false;

            return _context.RolPermisos.Any(rp =>
                rp.RolId == rol.Id &&
                rp.Permiso != null &&
                rp.Permiso.Modulo == modulo &&
                rp.Permiso.Accion == accion);
        }
    }
}