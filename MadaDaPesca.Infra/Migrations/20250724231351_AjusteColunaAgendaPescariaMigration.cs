using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjusteColunaAgendaPescariaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HoraInicio",
                table: "AgendaPescarias",
                newName: "QuantidadeDePescador");

            migrationBuilder.AddColumn<short>(
                name: "HoraInicial",
                table: "AgendaPescarias",
                type: "smallint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraInicial",
                table: "AgendaPescarias");

            migrationBuilder.RenameColumn(
                name: "QuantidadeDePescador",
                table: "AgendaPescarias",
                newName: "HoraInicio");
        }
    }
}
