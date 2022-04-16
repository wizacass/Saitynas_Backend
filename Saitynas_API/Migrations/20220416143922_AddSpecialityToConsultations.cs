using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saitynas_API.Migrations
{
    public partial class AddSpecialityToConsultations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestedSpecialityId",
                table: "Consultations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_RequestedSpecialityId",
                table: "Consultations",
                column: "RequestedSpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Specialities_RequestedSpecialityId",
                table: "Consultations",
                column: "RequestedSpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Specialities_RequestedSpecialityId",
                table: "Consultations");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_RequestedSpecialityId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "RequestedSpecialityId",
                table: "Consultations");
        }
    }
}
