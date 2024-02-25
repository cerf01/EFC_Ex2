using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFC_Ex2.DAL.Migrations
{
    public partial class AddedGoalsCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HittedGoals",
                table: "SoccerTeams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MissedGoals",
                table: "SoccerTeams",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HittedGoals",
                table: "SoccerTeams");

            migrationBuilder.DropColumn(
                name: "MissedGoals",
                table: "SoccerTeams");
        }
    }
}
