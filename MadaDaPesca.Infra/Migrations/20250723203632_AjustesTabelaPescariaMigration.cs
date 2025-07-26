using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjustesTabelaPescariaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempoDeDuracao",
                table: "Pescarias");

            migrationBuilder.AlterColumn<short>(
                name: "QuantidadePescador",
                table: "Pescarias",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<short>(
                name: "HoraFinal",
                table: "Pescarias",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "HoraInicial",
                table: "Pescarias",
                type: "smallint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraFinal",
                table: "Pescarias");

            migrationBuilder.DropColumn(
                name: "HoraInicial",
                table: "Pescarias");

            migrationBuilder.AlterColumn<int>(
                name: "QuantidadePescador",
                table: "Pescarias",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TempoDeDuracao",
                table: "Pescarias",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
