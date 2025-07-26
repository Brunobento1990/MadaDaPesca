using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AgendaPescariaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgendaPescarias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Dia = table.Column<short>(type: "smallint", nullable: false),
                    Mes = table.Column<short>(type: "smallint", nullable: false),
                    Ano = table.Column<short>(type: "smallint", nullable: false),
                    HoraInicio = table.Column<short>(type: "smallint", nullable: true),
                    HoraFinal = table.Column<short>(type: "smallint", nullable: true),
                    Observacao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PescariaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaPescarias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaPescarias_Pescarias_PescariaId",
                        column: x => x.PescariaId,
                        principalTable: "Pescarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendaPescarias_Ano",
                table: "AgendaPescarias",
                column: "Ano");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaPescarias_Dia",
                table: "AgendaPescarias",
                column: "Dia");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaPescarias_Dia_Mes_Ano",
                table: "AgendaPescarias",
                columns: new[] { "Dia", "Mes", "Ano" });

            migrationBuilder.CreateIndex(
                name: "IX_AgendaPescarias_Excluido",
                table: "AgendaPescarias",
                column: "Excluido");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaPescarias_Mes",
                table: "AgendaPescarias",
                column: "Mes");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaPescarias_PescariaId",
                table: "AgendaPescarias",
                column: "PescariaId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaPescarias_Status",
                table: "AgendaPescarias",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaPescarias");
        }
    }
}
