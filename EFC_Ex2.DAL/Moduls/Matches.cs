
namespace EFC_Ex2.DAL.Moduls
{
    public class Matches
    {
        public int Id { get; set; }

        public List<SoccerTeams> Teams { get; set; }

        public int HittedGoalsByTeam1 { get; set; }

        public int HittedGoalsByTeam2 { get; set; }

        public string Winner { get; set; }

        public DateTime DateOfMatch { get; set; }
    }
}
