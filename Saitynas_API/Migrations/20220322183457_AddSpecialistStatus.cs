using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saitynas_API.Migrations
{
    public partial class AddSpecialistStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecialistStatusId",
                table: "Specialists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpecialistStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialistStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SpecialistStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Offline" });

            migrationBuilder.InsertData(
                table: "SpecialistStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Available" });

            migrationBuilder.InsertData(
                table: "SpecialistStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Busy" });

            migrationBuilder.CreateIndex(
                name: "IX_Specialists_SpecialistStatusId",
                table: "Specialists",
                column: "SpecialistStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_SpecialistStatus_SpecialistStatusId",
                table: "Specialists",
                column: "SpecialistStatusId",
                principalTable: "SpecialistStatus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_SpecialistStatus_SpecialistStatusId",
                table: "Specialists");

            migrationBuilder.DropTable(
                name: "SpecialistStatus");

            migrationBuilder.DropIndex(
                name: "IX_Specialists_SpecialistStatusId",
                table: "Specialists");

            migrationBuilder.DropColumn(
                name: "SpecialistStatusId",
                table: "Specialists");
        }
    }
}
