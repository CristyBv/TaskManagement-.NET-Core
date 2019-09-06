using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagement.Data.Migrations
{
    public partial class addidtoteamProjectmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamProjects",
                table: "TeamProjects");

            migrationBuilder.AddColumn<int>(
                name: "IdTeamProject",
                table: "TeamProjects",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamProjects",
                table: "TeamProjects",
                column: "IdTeamProject");

            migrationBuilder.CreateIndex(
                name: "IX_TeamProjects_IdTeam",
                table: "TeamProjects",
                column: "IdTeam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamProjects",
                table: "TeamProjects");

            migrationBuilder.DropIndex(
                name: "IX_TeamProjects_IdTeam",
                table: "TeamProjects");

            migrationBuilder.DropColumn(
                name: "IdTeamProject",
                table: "TeamProjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamProjects",
                table: "TeamProjects",
                column: "IdTeam");
        }
    }
}
