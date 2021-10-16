using Microsoft.EntityFrameworkCore.Migrations;

namespace Saitynas_API.Migrations
{
    public partial class AddWorkplaceConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Workplaces_WorkplaceId",
                table: "Specialists");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Specialists",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Workplaces_WorkplaceId",
                table: "Specialists",
                column: "WorkplaceId",
                principalTable: "Workplaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Workplaces_WorkplaceId",
                table: "Specialists");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Specialists",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Workplaces_WorkplaceId",
                table: "Specialists",
                column: "WorkplaceId",
                principalTable: "Workplaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
