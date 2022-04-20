using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saitynas_API.Migrations
{
    public partial class AddGuidToConsultation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PublicId",
                table: "Consultations",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid(),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_PublicId",
                table: "Consultations",
                column: "PublicId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Consultations_PublicId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Consultations");
        }
    }
}
