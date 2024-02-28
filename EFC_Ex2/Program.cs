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
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Commands list:\n -show\n -add\n -upd\n -find\n -exit\n -maxWins\n -maxDef\n -maxDraw\n -maxGoals\n -maxMiss");
                q = Console.ReadLine();
                switch (q.ToLower())
                {
                    case "1":
                    case "-show":
                        {
                            ShowData();
                        }
                        break;
                    case "2":
                    case "-add":
                        {
                            AddData();
                        }
                        break;
                    case "3":
                    case "-upd":
                        {
                            UpdateData();
                        }
                        break;
                    case "4":
                    case "-del":
                        {
                            DeleteData();
                        }
                        break;
                    case "5":
                    case "-find":
                        {
                            FindInfo();
                        }
                        break;
                    case "6":
                    case "-maxWins":
                        {
                            ShowMaxThings(0);
                        }
                        break;
                    case "7":
                    case "-maxDef":
                        {
                            ShowMaxThings(1);
                        }
                        break;
                    case "8":
                    case "-maxDraw":
                        {
                            ShowMaxThings(2);
                        }
                        break;
                    case "9":
                    case "-maxGoals":
                        {
                            ShowMaxThings(3);
                        }
                        break;
                    case "10":
                    case "-maxMiss":
                        {
                            ShowMaxThings(4);
                        }
                        break;
                    default:
                        Console.WriteLine("wrong input!");
                        break;
                    case "q":
                    case "-exit":
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
            using (var context = new Context())
            {
                var teamsInfo = context.SoccerTeams;
                Console.WriteLine("Team |  City | Wins | Defeats | Draw | Hitted Goals | Missed Goals");
                foreach (var team in teamsInfo)
                {
                    Console.WriteLine($"{team.Name} | {team.City} | {team.WinCount} | {team.DefCount} | {team.DrawCount} | {team.HittedGoals} | {team.MissedGoals}");
                }

            }
        }

        private static void ShowMaxThings(int stage)
        {
            using (var context = new Context())
            {
                var teamsInfo = context.SoccerTeams;

                switch (stage)
                {
                    case 0:
                        {
                            var output = teamsInfo.Where(e => e.WinCount == teamsInfo.Max(c => c.WinCount)).First();
                            Console.WriteLine(output.Name);
                        }
                        break;
                    case 1:
                        {
                            var output = teamsInfo.Where(e => e.WinCount == teamsInfo.Max(c => c.DefCount)).First();
                            Console.WriteLine(output.Name);
                        }
                        break;
                    case 2:
                        {
                            var output = teamsInfo.Where(e => e.WinCount == teamsInfo.Max(c => c.DrawCount)).First();
                            Console.WriteLine(output.Name);
                        }
                        break;
                    case 3:
                        {
                            var output = teamsInfo.Where(e => e.WinCount == teamsInfo.Max(c => c.HittedGoals)).First();
                            Console.WriteLine(output.Name);
                        }
                        break;
                    case 4:
                        {
                            var output = teamsInfo.Where(e => e.WinCount == teamsInfo.Max(c => c.MissedGoals)).First();
                            Console.WriteLine(output.Name);
                        }
                        break;
                }

            }
        }

        private static void FindInfo()
        {
            using (var context = new Context())
            {
                var teamsInfo = context.SoccerTeams;
                var input = UserPrompt("Enter teams name/city");
                var output = teamsInfo.Where(e => e.Name.ToLower() == input.ToLower() || e.City.ToLower() == input.ToLower());
                Console.WriteLine("Team |  City | Wins | Defeats | Draw | Hitted Goals | Missed Goals");
                foreach (var item in output)
                {
                    Console.WriteLine($"{item.Name} | {item.City} | {item.WinCount} | {item.DefCount} | {item.DrawCount} | {item.HittedGoals} | {item.MissedGoals}");

                }
            }
        }

        private static T? ParseData<T>(string str) where T : struct
        {
            var type = typeof(T);
            if (type == typeof(int) && int.TryParse(str, out var val))
                return (T)(object)val;
            return null;
        }

        private static string UserPrompt(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        private static SoccerTeams MapData()
        {
            var team = new SoccerTeams()
            {

                Name = UserPrompt("Enter teams name"),
                City = UserPrompt("Enter teams city"),
                WinCount = (int)ParseData<int>(UserPrompt("Enter number of wins")),
                DefCount = (int)ParseData<int>(UserPrompt("Enter number of defeats")),
                DrawCount = (int)ParseData<int>(UserPrompt("Enter number of draws")),
                HittedGoals = ParseData<int>(UserPrompt("Enter number of  hitted goals")),
                MissedGoals = ParseData<int>(UserPrompt("Enter number of  missed goals")),

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

        private static void DeleteData()
        {
            using (var context = new Context())
            {
                int? teamId = ParseData<int>(UserPrompt("Enter id of team"));
                if (teamId == null)
                    return;

                var team = context.SoccerTeams.Where(e => e.Id == teamId).First();

                context.SoccerTeams.Remove(team);
                context.SaveChanges();
            }
        }

        private static void UpdateData()
        {
            using (var context = new Context())
            {
                int? teamId = ParseData<int>(UserPrompt("Enter id of team"));
                if (teamId == null)
                    return;

                int? selector = ParseData<int>(UserPrompt("Select field to update:\n 1 - Name\n2 - City\n3 - Wins\n4 - Defeats\n5 - Draw\n 6 - Hitted Goals\n7 - Missed Goals"));
                if (selector == null)
                    return;
                var team = context.SoccerTeams.Where(e => e.Id == teamId).First();

                switch (selector)
                {
                    case 1:
                        team.Name = UserPrompt("Enter teams name");
                        break;
                    case 2:
                        team.City = UserPrompt("Enter teams city");
                        break;
                    case 3:
                        team.WinCount = (int)ParseData<int>(UserPrompt("Enter number of wins")); break;
                    case 4:
                        team.DefCount = (int)ParseData<int>(UserPrompt("Enter number of defeats")); break;
                    case 5:
                        team.DrawCount = (int)ParseData<int>(UserPrompt("Enter number of draws")); break;
                    case 6:
                        team.HittedGoals = ParseData<int>(UserPrompt("Enter number of  hitted goals")); break;
                    case 7:
                        team.MissedGoals = ParseData<int>(UserPrompt("Enter number of  missed goals"));
                        break;
                }

                context.SoccerTeams.Update(team);
                context.SaveChanges();

            }
        }
    }
}