using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedOne.Web.Migrations
{
    /// <inheritdoc />
    public partial class AgregaSolicitudesContacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitudesContacto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    TipoCentro = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Mensaje = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    AceptaContacto = table.Column<bool>(type: "boolean", nullable: false),
                    Atendida = table.Column<bool>(type: "boolean", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesContacto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesContacto_Fecha",
                table: "SolicitudesContacto",
                column: "Fecha");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudesContacto");
        }
    }
}
