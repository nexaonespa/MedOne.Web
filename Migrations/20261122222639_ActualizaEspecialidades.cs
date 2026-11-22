using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedOne.Web.Migrations
{
    /// <inheritdoc />
    public partial class ActualizaEspecialidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Médico");

            migrationBuilder.UpdateData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Psicólogo");

            migrationBuilder.UpdateData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "Kinesiólogo");

            migrationBuilder.InsertData(
                table: "Especialidades",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 4, "Fonoaudiólogo" },
                    { 5, "Terapeuta Ocupacional" },
                    { 6, "Neuropsicólogo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Medicina General");

            migrationBuilder.UpdateData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Salud Mental");

            migrationBuilder.UpdateData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "Kinesiología");
        }
    }
}
