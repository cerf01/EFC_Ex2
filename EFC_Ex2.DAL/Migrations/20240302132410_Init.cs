using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFC_Ex2.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HittedGoalsByTeam1 = table.Column<int>(type: "int", nullable: false),
                    HittedGoalsByTeam2 = table.Column<int>(type: "int", nullable: false),
                    Winner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfMatch = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoccerTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WinCount = table.Column<int>(type: "int", nullable: false),
                    DefCount = table.Column<int>(type: "int", nullable: false),
                    DrawCount = table.Column<int>(type: "int", nullable: false),
                    HittedGoals = table.Column<int>(type: "int", nullable: true),
                    MissedGoals = table.Column<int>(type: "int", nullable: true),
                    MatchesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoccerTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoccerTeams_Matches_MatchesId",
                        column: x => x.MatchesId,
                        principalTable: "Matches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoccerTeamCmp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoccerTeamCmp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoccerTeamCmp_SoccerTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "SoccerTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoccerTeamCmp_TeamId",
                table: "SoccerTeamCmp",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_SoccerTeams_MatchesId",
                table: "SoccerTeams",
                column: "MatchesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoccerTeamCmp");

            migrationBuilder.DropTable(
                name: "SoccerTeams");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
