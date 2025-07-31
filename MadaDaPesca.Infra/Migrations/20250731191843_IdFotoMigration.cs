using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class IdFotoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdFoto",
                table: "Pessoas",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFoto",
                table: "Pessoas");
        }
    }
}
