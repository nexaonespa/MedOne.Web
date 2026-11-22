using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedOne.Web.Migrations
{
    /// <inheritdoc />
    public partial class AgregaAccesoClinicoTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccesoClinicoTotal",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccesoClinicoTotal",
                table: "Usuarios");
        }
    }
}
