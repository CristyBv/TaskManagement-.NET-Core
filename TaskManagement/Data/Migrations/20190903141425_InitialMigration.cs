using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagement.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdEchipa",
                table: "AspNetUsers",
                nullable: true);

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
                    Titlu = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    IdEchipa = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    IdTask = table.Column<string>(nullable: false),
                    Titlu = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Prioritate = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    IdCreator = table.Column<string>(nullable: true),
                    IdMembru = table.Column<string>(nullable: true),
                    IdProiect = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.IdTask);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_IdCreator",
                        column: x => x.IdCreator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_IdMembru",
                        column: x => x.IdMembru,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Proiecte_IdProiect",
                        column: x => x.IdProiect,
                        principalTable: "Proiecte",
                        principalColumn: "IdProiect",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdEchipa",
                table: "AspNetUsers",
                column: "IdEchipa");

            migrationBuilder.CreateIndex(
                name: "IX_Proiecte_IdEchipa",
                table: "Proiecte",
                column: "IdEchipa");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IdCreator",
                table: "Tasks",
                column: "IdCreator");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IdMembru",
                table: "Tasks",
                column: "IdMembru");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IdProiect",
                table: "Tasks",
                column: "IdProiect");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Echipe_IdEchipa",
                table: "AspNetUsers",
                column: "IdEchipa",
                principalTable: "Echipe",
                principalColumn: "IdEchipa",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Echipe_IdEchipa",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Proiecte");

            migrationBuilder.DropTable(
                name: "Echipe");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdEchipa",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdEchipa",
                table: "AspNetUsers");
        }
    }
}
