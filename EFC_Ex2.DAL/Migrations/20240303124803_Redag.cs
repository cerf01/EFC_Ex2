using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFC_Ex2.DAL.Migrations
{
    public partial class Redag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_SoccerTeams_Teams1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_SoccerTeams_Teams2Id",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Teams2Id",
                table: "Matches",
                newName: "Team2Id");

            migrationBuilder.RenameColumn(
                name: "Teams1Id",
                table: "Matches",
                newName: "Team1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Teams2Id",
                table: "Matches",
                newName: "IX_Matches_Team2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Teams1Id",
                table: "Matches",
                newName: "IX_Matches_Team1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_SoccerTeams_Team1Id",
                table: "Matches",
                column: "Team1Id",
                principalTable: "SoccerTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_SoccerTeams_Team2Id",
                table: "Matches",
                column: "Team2Id",
                principalTable: "SoccerTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_SoccerTeams_Team1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_SoccerTeams_Team2Id",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Team2Id",
                table: "Matches",
                newName: "Teams2Id");

            migrationBuilder.RenameColumn(
                name: "Team1Id",
                table: "Matches",
                newName: "Teams1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Team2Id",
                table: "Matches",
                newName: "IX_Matches_Teams2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Team1Id",
                table: "Matches",
                newName: "IX_Matches_Teams1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_SoccerTeams_Teams1Id",
                table: "Matches",
                column: "Teams1Id",
                principalTable: "SoccerTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_SoccerTeams_Teams2Id",
                table: "Matches",
                column: "Teams2Id",
                principalTable: "SoccerTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
