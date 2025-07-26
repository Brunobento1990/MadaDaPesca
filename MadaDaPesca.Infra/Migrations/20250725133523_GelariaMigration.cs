using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class GelariaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GaleriaAgendaPescaria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    AgendaPescariaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GaleriaAgendaPescaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GaleriaAgendaPescaria_AgendaPescarias_AgendaPescariaId",
                        column: x => x.AgendaPescariaId,
                        principalTable: "AgendaPescarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GaleriaAgendaPescaria_AgendaPescariaId",
                table: "GaleriaAgendaPescaria",
                column: "AgendaPescariaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GaleriaAgendaPescaria");
        }
    }
}
