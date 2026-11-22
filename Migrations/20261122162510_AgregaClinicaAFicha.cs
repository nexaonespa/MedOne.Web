using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedOne.Web.Migrations
{
    /// <inheritdoc />
    public partial class AgregaClinicaAFicha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClinicaId",
                table: "Fichas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Fichas_ClinicaId",
                table: "Fichas",
                column: "ClinicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fichas_Clinicas_ClinicaId",
                table: "Fichas",
                column: "ClinicaId",
                principalTable: "Clinicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fichas_Clinicas_ClinicaId",
                table: "Fichas");

            migrationBuilder.DropIndex(
                name: "IX_Fichas_ClinicaId",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "ClinicaId",
                table: "Fichas");
        }
    }
}
