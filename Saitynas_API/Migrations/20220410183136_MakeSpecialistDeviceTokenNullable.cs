using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saitynas_API.Migrations
{
    public partial class MakeSpecialistDeviceTokenNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpecialistDeviceToken",
                table: "Consultations",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Consultations",
                keyColumn: "SpecialistDeviceToken",
                keyValue: null,
                column: "SpecialistDeviceToken",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialistDeviceToken",
                table: "Consultations",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
