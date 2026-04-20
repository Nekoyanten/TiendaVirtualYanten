using Microsoft.EntityFrameworkCore;
using TiendaVirtualYanten.Models;

namespace TiendaVirtualYanten.Data
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed de permisos base al crear la BD
            var modulos = new[] { "Producto", "Categoria", "Usuario", "Rol" };
            var acciones = new[] { "Ver", "Crear", "Editar", "Eliminar" };

            int id = 1;
            foreach (var modulo in modulos)
                foreach (var accion in acciones)
                    modelBuilder.Entity<Permiso>().HasData(
                        new Permiso { Id = id++, Modulo = modulo, Accion = accion });
        }
    }
}
