using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class LatLngPescariaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Latitude",
                table: "Pescarias",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Longitude",
                table: "Pescarias",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Pescarias");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Pescarias");
        }
    }
}
