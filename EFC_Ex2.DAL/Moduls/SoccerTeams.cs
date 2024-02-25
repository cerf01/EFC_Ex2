
namespace EFC_Ex2.DAL.Moduls
{
    public class SoccerTeams
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City{ get; set; }
        
        public int WinCount{ get; set; }

        public int DefCount { get; set; }

        public int DrawCount { get; set; }

        public int? HittedGoals { get; set; }

        public int? MissedGoals { get; set; }
    }
}
