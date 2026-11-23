using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedOne.Web.Migrations
{
    /// <inheritdoc />
    public partial class FichasClinicas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fichas_Citas_CitaId",
                table: "Fichas");

            migrationBuilder.DropForeignKey(
                name: "FK_Fichas_Pacientes_PacienteId",
                table: "Fichas");

            migrationBuilder.DropIndex(
                name: "IX_Fichas_CitaId",
                table: "Fichas");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Fichas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfesionalId",
                table: "Fichas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FichaVersiones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClinicaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FichaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DatosJson = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    UsuarioNombre = table.Column<string>(type: "text", nullable: false),
                    FechaReemplazo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaVersiones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fichas_CitaId",
                table: "Fichas",
                column: "CitaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fichas_ProfesionalId",
                table: "Fichas",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaVersiones_FichaId",
                table: "FichaVersiones",
                column: "FichaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fichas_Citas_CitaId",
                table: "Fichas",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fichas_Pacientes_PacienteId",
                table: "Fichas",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fichas_Profesionales_ProfesionalId",
                table: "Fichas",
                column: "ProfesionalId",
                principalTable: "Profesionales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fichas_Citas_CitaId",
                table: "Fichas");

            migrationBuilder.DropForeignKey(
                name: "FK_Fichas_Pacientes_PacienteId",
                table: "Fichas");

            migrationBuilder.DropForeignKey(
                name: "FK_Fichas_Profesionales_ProfesionalId",
                table: "Fichas");

            migrationBuilder.DropTable(
                name: "FichaVersiones");

            migrationBuilder.DropIndex(
                name: "IX_Fichas_CitaId",
                table: "Fichas");

            migrationBuilder.DropIndex(
                name: "IX_Fichas_ProfesionalId",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "ProfesionalId",
                table: "Fichas");

            migrationBuilder.CreateIndex(
                name: "IX_Fichas_CitaId",
                table: "Fichas",
                column: "CitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fichas_Citas_CitaId",
                table: "Fichas",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fichas_Pacientes_PacienteId",
                table: "Fichas",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
