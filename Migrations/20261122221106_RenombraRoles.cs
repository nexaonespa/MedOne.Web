using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedOne.Web.Migrations
{
    /// <inheritdoc />
    public partial class RenombraRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Actualiza los roles ya guardados a los nombres nuevos
            migrationBuilder.Sql("UPDATE \"Usuarios\" SET \"Rol\" = 'Administrador' WHERE \"Rol\" = 'AdminClinica';");
            migrationBuilder.Sql("UPDATE \"Usuarios\" SET \"Rol\" = 'Coordinador' WHERE \"Rol\" = 'Recepcion';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Permite deshacer el cambio si fuera necesario
            migrationBuilder.Sql("UPDATE \"Usuarios\" SET \"Rol\" = 'AdminClinica' WHERE \"Rol\" = 'Administrador';");
            migrationBuilder.Sql("UPDATE \"Usuarios\" SET \"Rol\" = 'Recepcion' WHERE \"Rol\" = 'Coordinador';");
        }
    }
}