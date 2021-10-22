using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Saitynas_API.Migrations
{
    public partial class AddEvaluations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    SpecialistId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_Specialists_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Specialists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_SpecialistId",
                table: "Evaluations",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_UserId",
                table: "Evaluations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");
        }
    }
}
