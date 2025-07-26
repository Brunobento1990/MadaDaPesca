using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoPescariaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BloquearDomingo",
                table: "Pescarias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BloquearQuartaFeira",
                table: "Pescarias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BloquearQuintaFeira",
                table: "Pescarias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BloquearSabado",
                table: "Pescarias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BloquearSegundaFeira",
                table: "Pescarias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BloquearSextaFeira",
                table: "Pescarias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BloquearTercaFeira",
                table: "Pescarias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<short>(
                name: "QuantidadeMaximaDeAgendamentosNoDia",
                table: "Pescarias",
                type: "smallint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloquearDomingo",
                table: "Pescarias");

            migrationBuilder.DropColumn(
                name: "BloquearQuartaFeira",
                table: "Pescarias");

            migrationBuilder.DropColumn(
                name: "BloquearQuintaFeira",
                table: "Pescarias");

            migrationBuilder.DropColumn(
                name: "BloquearSabado",
                table: "Pescarias");

            migrationBuilder.DropColumn(
                name: "BloquearSegundaFeira",
                table: "Pescarias");

            migrationBuilder.DropColumn(
                name: "BloquearSextaFeira",
                table: "Pescarias");

            migrationBuilder.DropColumn(
                name: "BloquearTercaFeira",
                table: "Pescarias");

            migrationBuilder.DropColumn(
                name: "QuantidadeMaximaDeAgendamentosNoDia",
                table: "Pescarias");
        }
    }
}
