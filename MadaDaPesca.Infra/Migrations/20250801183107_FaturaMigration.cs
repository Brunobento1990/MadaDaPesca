using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class FaturaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaturasAgendaPescarias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AgendaPescariaId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuiaDePescaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeVencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturasAgendaPescarias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaturasAgendaPescarias_AgendaPescarias_AgendaPescariaId",
                        column: x => x.AgendaPescariaId,
                        principalTable: "AgendaPescarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturasAgendaPescarias_GuiasDePesca_GuiaDePescaId",
                        column: x => x.GuiaDePescaId,
                        principalTable: "GuiasDePesca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransacoesFaturaAgenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FaturaAgendaPescariaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MeioDePagamento = table.Column<int>(type: "integer", nullable: false),
                    TipoTransacao = table.Column<int>(type: "integer", nullable: false),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacoesFaturaAgenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransacoesFaturaAgenda_FaturasAgendaPescarias_FaturaAgendaP~",
                        column: x => x.FaturaAgendaPescariaId,
                        principalTable: "FaturasAgendaPescarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaturasAgendaPescarias_AgendaPescariaId",
                table: "FaturasAgendaPescarias",
                column: "AgendaPescariaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaturasAgendaPescarias_Excluido",
                table: "FaturasAgendaPescarias",
                column: "Excluido");

            migrationBuilder.CreateIndex(
                name: "IX_FaturasAgendaPescarias_GuiaDePescaId",
                table: "FaturasAgendaPescarias",
                column: "GuiaDePescaId");

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesFaturaAgenda_Excluido",
                table: "TransacoesFaturaAgenda",
                column: "Excluido");

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesFaturaAgenda_FaturaAgendaPescariaId",
                table: "TransacoesFaturaAgenda",
                column: "FaturaAgendaPescariaId");

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesFaturaAgenda_MeioDePagamento",
                table: "TransacoesFaturaAgenda",
                column: "MeioDePagamento");

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesFaturaAgenda_TipoTransacao",
                table: "TransacoesFaturaAgenda",
                column: "TipoTransacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransacoesFaturaAgenda");

            migrationBuilder.DropTable(
                name: "FaturasAgendaPescarias");
        }
    }
}
