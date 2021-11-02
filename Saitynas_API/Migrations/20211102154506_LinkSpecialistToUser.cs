using Microsoft.EntityFrameworkCore.Migrations;

namespace Saitynas_API.Migrations
{
    public partial class LinkSpecialistToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecialistId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SpecialistId",
                table: "Users",
                column: "SpecialistId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Specialists_SpecialistId",
                table: "Users",
                column: "SpecialistId",
                principalTable: "Specialists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Specialists_SpecialistId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SpecialistId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpecialistId",
                table: "Users");
        }
    }
}
