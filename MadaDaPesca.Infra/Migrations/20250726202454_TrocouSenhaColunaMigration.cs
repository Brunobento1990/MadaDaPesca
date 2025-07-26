using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadaDaPesca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TrocouSenhaColunaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TrocouSenha",
                table: "AcessosGuiasDePesca",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrocouSenha",
                table: "AcessosGuiasDePesca");
        }
    }
}
