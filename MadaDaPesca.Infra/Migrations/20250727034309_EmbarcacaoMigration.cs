using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class EmbarcacaoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmbarcacaoId",
                table: "Pescarias",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Embarcacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Motor = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MotorEletrico = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Largura = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Comprimento = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    QuantidadeDeLugar = table.Column<short>(type: "smallint", nullable: true),
                    GuiaDePescaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Embarcacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Embarcacoes_GuiasDePesca_GuiaDePescaId",
                        column: x => x.GuiaDePescaId,
                        principalTable: "GuiasDePesca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pescarias_EmbarcacaoId",
                table: "Pescarias",
                column: "EmbarcacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Embarcacoes_Excluido",
                table: "Embarcacoes",
                column: "Excluido");

            migrationBuilder.CreateIndex(
                name: "IX_Embarcacoes_GuiaDePescaId",
                table: "Embarcacoes",
                column: "GuiaDePescaId");

            migrationBuilder.CreateIndex(
                name: "IX_Embarcacoes_Nome",
                table: "Embarcacoes",
                column: "Nome");

            migrationBuilder.AddForeignKey(
                name: "FK_Pescarias_Embarcacoes_EmbarcacaoId",
                table: "Pescarias",
                column: "EmbarcacaoId",
                principalTable: "Embarcacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pescarias_Embarcacoes_EmbarcacaoId",
                table: "Pescarias");

            migrationBuilder.DropTable(
                name: "Embarcacoes");

            migrationBuilder.DropIndex(
                name: "IX_Pescarias_EmbarcacaoId",
                table: "Pescarias");

            migrationBuilder.DropColumn(
                name: "EmbarcacaoId",
                table: "Pescarias");
        }
    }
}
