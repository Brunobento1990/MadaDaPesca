using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class GaleriaEmbarcacaoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GaleriaFotoEmbarcacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    EmbarcacaoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GaleriaFotoEmbarcacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GaleriaFotoEmbarcacoes_Embarcacoes_EmbarcacaoId",
                        column: x => x.EmbarcacaoId,
                        principalTable: "Embarcacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GaleriaFotoEmbarcacoes_EmbarcacaoId",
                table: "GaleriaFotoEmbarcacoes",
                column: "EmbarcacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GaleriaFotoEmbarcacoes");
        }
    }
}
