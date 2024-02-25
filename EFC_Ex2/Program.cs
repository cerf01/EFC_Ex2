using EFC_Ex2.DAL;
using EFC_Ex2.DAL.Moduls;

namespace EFC_Ex2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string q = "";
            do
            {
                q = Console.ReadLine();
                switch (q.ToLower())
                {
                    case "1":
                    case "show":
                        {
                            ShowData();
                        }
                        break;

                    default:
                        Console.WriteLine("wrong input!");
                        break;
                    case "q":
                    case "exit":
                        {
                            Console.WriteLine("goodbye!");
                            q = "endoftime";
                        }
                        break;
                }
            } while (q != "endoftime");
            Console.ReadKey();
        }
        private static void ShowData() 
        {
            using(var context = new Context()) 
            {
                var teamsInfo = context.SoccerTeams;
                Console.WriteLine("Team |  City | Wins | Defeats | Draw | Hitted Goals | Missed Goals");
                foreach (var team in teamsInfo) 
                {
                    Console.WriteLine($"{team.Name} | {team.City} | {team.WinCount} | {team.DefCount} | {team.DrawCount} | {team.HittedGoals} | {team.MissedGoals}");
                }

            }
        }

        private static T ParseData<T>(string str) where T : struct 
        { 
            var type = typeof(T); 
            if (type == typeof(int) && int.TryParse(str, out var val)) 
                return (T)(object)val; 
            return (T)(object)val;
        }

        private static string UserPrompt(string prompt) 
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        private static SoccerTeams MapData()
        {
            var team = new SoccerTeams() {

                Name = UserPrompt("Enter teams name"),
                City = UserPrompt("Enter teams city"),
                WinCount = ParseData<int>(UserPrompt("Enter number of wins")),

            };

            return team;
        }
        private static void AddData()
        {
            using (var context = new Context())
            {
                context.SoccerTeams.Add(MapData());
                context.SaveChanges();
            }
        }

        private static void UpdateData()
        {
            using (var context = new Context())
            {

  
            }
        }
    }
}