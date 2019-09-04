using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagement.Data.Migrations
{
    public partial class updateconfigurationsandnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Echipe_IdEchipa",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_IdMembru",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Proiecte_IdProiect",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Proiecte");

            migrationBuilder.DropTable(
                name: "Echipe");

            migrationBuilder.RenameColumn(
                name: "Titlu",
                table: "Tasks",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Prioritate",
                table: "Tasks",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "IdProiect",
                table: "Tasks",
                newName: "IdProject");

            migrationBuilder.RenameColumn(
                name: "IdMembru",
                table: "Tasks",
                newName: "IdMember");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_IdProiect",
                table: "Tasks",
                newName: "IX_Tasks_IdProject");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_IdMembru",
                table: "Tasks",
                newName: "IX_Tasks_IdMember");

            migrationBuilder.RenameColumn(
                name: "IdEchipa",
                table: "AspNetUsers",
                newName: "IdTeam");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_IdEchipa",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_IdTeam");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    IdTeam = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.IdTeam);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    IdProject = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    IdTeam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.IdProject);
                    table.ForeignKey(
                        name: "FK_Projects_Teams_IdTeam",
                        column: x => x.IdTeam,
                        principalTable: "Teams",
                        principalColumn: "IdTeam",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_IdTeam",
                table: "Projects",
                column: "IdTeam");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_IdTeam",
                table: "AspNetUsers",
                column: "IdTeam",
                principalTable: "Teams",
                principalColumn: "IdTeam",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_IdMember",
                table: "Tasks",
                column: "IdMember",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_IdProject",
                table: "Tasks",
                column: "IdProject",
                principalTable: "Projects",
                principalColumn: "IdProject",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_IdTeam",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_IdMember",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_IdProject",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tasks",
                newName: "Titlu");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Tasks",
                newName: "Prioritate");

            migrationBuilder.RenameColumn(
                name: "IdProject",
                table: "Tasks",
                newName: "IdProiect");

            migrationBuilder.RenameColumn(
                name: "IdMember",
                table: "Tasks",
                newName: "IdMembru");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_IdProject",
                table: "Tasks",
                newName: "IX_Tasks_IdProiect");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_IdMember",
                table: "Tasks",
                newName: "IX_Tasks_IdMembru");

            migrationBuilder.RenameColumn(
                name: "IdTeam",
                table: "AspNetUsers",
                newName: "IdEchipa");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_IdTeam",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_IdEchipa");

            migrationBuilder.CreateTable(
                name: "Echipe",
                columns: table => new
                {
                    IdEchipa = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Echipe", x => x.IdEchipa);
                });

            migrationBuilder.CreateTable(
                name: "Proiecte",
                columns: table => new
                {
                    IdProiect = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Deadline = table.Column<DateTime>(nullable: false),
                    IdEchipa = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Titlu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proiecte", x => x.IdProiect);
                    table.ForeignKey(
                        name: "FK_Proiecte_Echipe_IdEchipa",
                        column: x => x.IdEchipa,
                        principalTable: "Echipe",
                        principalColumn: "IdEchipa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proiecte_IdEchipa",
                table: "Proiecte",
                column: "IdEchipa");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Echipe_IdEchipa",
                table: "AspNetUsers",
                column: "IdEchipa",
                principalTable: "Echipe",
                principalColumn: "IdEchipa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_IdMembru",
                table: "Tasks",
                column: "IdMembru",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Proiecte_IdProiect",
                table: "Tasks",
                column: "IdProiect",
                principalTable: "Proiecte",
                principalColumn: "IdProiect",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
