using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InicialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcessosGuiasDePesca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Senha = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    PrimeiroAcesso = table.Column<bool>(type: "boolean", nullable: false),
                    EmailVerificado = table.Column<bool>(type: "boolean", nullable: false),
                    AcessoBloqueado = table.Column<bool>(type: "boolean", nullable: false),
                    TokenEsqueceuSenha = table.Column<Guid>(type: "uuid", nullable: true),
                    ExpiracaoTokenEsqueceuSenha = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcessosGuiasDePesca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuiasDePesca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uuid", nullable: false),
                    AcessoGuiaDePescaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiasDePesca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuiasDePesca_AcessosGuiasDePesca_AcessoGuiaDePescaId",
                        column: x => x.AcessoGuiaDePescaId,
                        principalTable: "AcessosGuiasDePesca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuiasDePesca_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pescarias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Local = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    TempoDeDuracao = table.Column<int>(type: "integer", nullable: false),
                    QuantidadePescador = table.Column<int>(type: "integer", nullable: false),
                    GuiaDePescaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pescarias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pescarias_GuiasDePesca_GuiaDePescaId",
                        column: x => x.GuiaDePescaId,
                        principalTable: "GuiasDePesca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuiasDePesca_AcessoGuiaDePescaId",
                table: "GuiasDePesca",
                column: "AcessoGuiaDePescaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuiasDePesca_Excluido",
                table: "GuiasDePesca",
                column: "Excluido");

            migrationBuilder.CreateIndex(
                name: "IX_GuiasDePesca_PessoaId",
                table: "GuiasDePesca",
                column: "PessoaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pescarias_Excluido",
                table: "Pescarias",
                column: "Excluido");

            migrationBuilder.CreateIndex(
                name: "IX_Pescarias_GuiaDePescaId",
                table: "Pescarias",
                column: "GuiaDePescaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Cpf",
                table: "Pessoas",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Email",
                table: "Pessoas",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Excluido",
                table: "Pessoas",
                column: "Excluido");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Nome",
                table: "Pessoas",
                column: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pescarias");

            migrationBuilder.DropTable(
                name: "GuiasDePesca");

            migrationBuilder.DropTable(
                name: "AcessosGuiasDePesca");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
