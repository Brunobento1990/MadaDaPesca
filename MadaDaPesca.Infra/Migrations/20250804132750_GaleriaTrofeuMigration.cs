using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class GaleriaTrofeuMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GaleriaDeTrofeus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GuiaDePescaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GaleriaDeTrofeus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GaleriaDeTrofeus_GuiasDePesca_GuiaDePescaId",
                        column: x => x.GuiaDePescaId,
                        principalTable: "GuiasDePesca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GaleriaDeTrofeus_Excluido",
                table: "GaleriaDeTrofeus",
                column: "Excluido");

            migrationBuilder.CreateIndex(
                name: "IX_GaleriaDeTrofeus_GuiaDePescaId",
                table: "GaleriaDeTrofeus",
                column: "GuiaDePescaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GaleriaDeTrofeus");
        }
    }
}
