using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaVirtualYanten.Migrations
{
    /// <inheritdoc />
    public partial class QuitarSeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[] { 1, "Acceso total al sistema", "Admin" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Celular", "Correo", "Nombre", "Password", "RolId" },
                values: new object[] { 1, "3001234567", "admin@tienda.com", "Administrador", "$2a$11$jbJtmVyAB8A0cptjZge8QO6WyMMjLrUlSYATTVakDgI4XgR8UP8N.", 1 });
        }
    }
}
