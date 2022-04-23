using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saitynas_API.Migrations
{
    public partial class AddConsultationEvaluations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsultationId",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ConsultationId",
                table: "Evaluations",
                column: "ConsultationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Consultations_ConsultationId",
                table: "Evaluations",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Consultations_ConsultationId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ConsultationId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "ConsultationId",
                table: "Evaluations");
        }
    }
}
