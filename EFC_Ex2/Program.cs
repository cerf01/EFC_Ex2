using EFC_Ex2.DAL;
using EFC_Ex2.DAL.Moduls;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
/*using EFC_Ex2.DAL.Migrations;*/
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace EFC_Ex2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            AutoSetTeam1();

            AutoSetTeam2();

            AutoSetMatch();
    
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
                            ShowData(UserPrompt("Enter table"));
                        }
                        break;
                    case "2":
                    case "-add":
                        {
                            AddData(UserPrompt("Enter table"));
                          
                        }
                        break;
                    case "3":
                    case "-upd":
                        {
                            UpdateData();
                            Console.WriteLine("Item updated!");
                        }
                        break;
                    case "4":
                    case "-del":
                        {
                            DeleteData("1");
                            Console.WriteLine("Item deleted!");
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

        private static void ShowData(string tableName)
        {
            using (var context = new Context())
            {
                switch (tableName.ToLower())
                {
                    case "t":
                    case "teams":
                        {
                            var teamsInfo = context.SoccerTeams;
                            Console.WriteLine("Team |  City | Wins | Defeats | Draw | Hitted Goals | Missed Goals");
                            foreach (var team in teamsInfo)
                            {
                                Console.WriteLine($"{team.Name} | {team.City} | {team.WinCount} | {team.DefCount} | {team.DrawCount} | {team.HittedGoals} | {team.MissedGoals}");
                            }
                        }
                        break;
                    case "m":
                    case "matches":
                        {

                            var matchs = context.Matches
                                .Include(m => m.Teams1)
                                .Include(m => m.Teams2)
                                .ToList();

                            Console.WriteLine("Date of match | Team 1 | Team 2 | Hitted goals by Team 1 | Hitted goals by Team 2 | Winner ");
                            foreach (var match in matchs)
                                Console.WriteLine($"{match.DateOfMatch} | {match.Teams1.Name} | {match.Teams2.Name} | {match.HittedGoalsByTeam1} | {match.HittedGoalsByTeam2} | ");

                        }
                        break;
                    case "p":
                    case "player":
                        {
                            add(context, MapPlayer());
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("wrong input!");
                            return;
                        }
                        Console.WriteLine("Item added!");
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
            if (type == typeof(int) && int.TryParse(str, out var intVal))
                return (T)(object)intVal;
            if (type == typeof(DateTime) && DateTime.TryParse(str, out var dateVal))
                return (T)(object)dateVal;
            return null;
        }

        private static string UserPrompt(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

       

        private static SoccerTeams GetTeam(string prompt)
        {
            var input = prompt;
            using (var context = new Context())
            {
                var result = context.SoccerTeams.Where(t => t.Name.ToLower() == input.ToLower()).First();
                return result;
            }
         
        }

        private static SoccerTeamComposition GetPlayer(string prompt)
        {
            var input = prompt;
            using (var context = new Context())
            {
                var result = context.SoccerTeamCmp.Where(p => p.FullName.ToLower().Contains(input.ToLower())).First();
                return result;
            }

        }

        private static Matches GetMatch(string prompt) 
        {
            var input = ParseData<DateTime>(prompt);
            using (var context = new Context()) 
            {
                var result = context.Matches.Where(m => m.DateOfMatch.Equals(input)).First();
                return result;
            }
        }

        private static SoccerTeams MapSoccerTeam()
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

        private static SoccerTeamComposition MapPlayer() 
        {
            var palayer = new SoccerTeamComposition() 
            {
                FullName = UserPrompt("Enter plaeyrs fullname"),
                Country = UserPrompt("Enter players country"),
                Team = GetTeam(UserPrompt("Enter teams name")),
                Position = UserPrompt("Enter players position")
            };
            return palayer;
        }

        private static Matches MapMatch() 
        {
            var match = new Matches()
            {
                Teams1 = GetTeam(UserPrompt("Enter team 1 name")),
                Teams2 = GetTeam(UserPrompt("Enter team 2 name")),
                DateOfMatch = (DateTime)ParseData<DateTime>(UserPrompt("Enter date of this match")),
                HittedGoalsByTeam1 = (int)ParseData<int>(UserPrompt("Enter hitted goals by team 1")),
                HittedGoalsByTeam2 = (int)ParseData<int>(UserPrompt("Enter hitted goals by team 2")),
                Winner = UserPrompt("Enter winner of this match"),
            };
          
            return match;
        }

        private static void AutoSetTeam1() 
        {
          var team = new SoccerTeams()
          {
              Name = "Team1",
              City = "City17",
              WinCount = 13,
              DefCount = 8,
              DrawCount = 4,
              HittedGoals = 30,
              MissedGoals = 18,
          };

            add(new Context(), team);
        }


        private static void AutoSetTeam2()
        {
            var team = new SoccerTeams()
            {
                Name = "14Gorilaz",
                City = "NewYotus",
                WinCount = 15,
                DefCount = 12,
                DrawCount = 7,
                HittedGoals = 19,
                MissedGoals = 14,
            };

            add(new Context(), team);
        }

        private static void AutoSetMatch() 
        {

            add(new Context(), new Matches()
            {
                Teams1 = GetTeam("Team1"),
                Teams2 = GetTeam("14Gorilaz"),
                DateOfMatch = (DateTime)ParseData<DateTime>("2023-12-12"),
                HittedGoalsByTeam1 =12,
                HittedGoalsByTeam2 =10,
                Winner = "Team1",
            }) ;

        }

        private static void AddData(string tableName)
        {
            using (var context = new Context())
            {
                switch (tableName.ToLower())
                {
                    case "t":
                    case "team":
                        {
                            add(context, MapSoccerTeam());
                        }
                        break;
                    case "m":
                    case "match":
                        {
                      
                            add(context, MapMatch());
                        }
                        break;
                    case "p":
                    case "player":
                        {
                            add(context, MapPlayer());
                        }
                        break;
                    default: 
                        {
                            Console.WriteLine("wrong input!");
                            return;
                        }
                }

            }
        }


        private static void DeleteData(string tableName)
        {
            using (var context = new Context())
            {
                switch (tableName)
                {
                    case "teams":
                        {
                            delete(context, GetTeam(UserPrompt("Enrter teams name")));
                            }
                        break;
                    case "macthse":
                        {

                            delete(context, GetMatch(UserPrompt("Enter date of  match")));
                        }break;
                    case "player":
                        {
                            delete(context, GetPlayer(UserPrompt("Enter palayers name")));
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("wrong input!");
                            return;
                        }

                }
               
            }
        }
        private static void add(Context context, object entity) 
        {
            context.Add(entity);
            context.SaveChanges();
        }     

        private static void delete(Context context, object entity)
        {

                context.Remove(entity);
                context.SaveChanges();
        }

        private static void UpdateData()
        {
            using (var context = new Context())
            {
                

               var selector = ParseData<int>(UserPrompt("Select field to update:\n 1 - Name\n2 - City\n3 - Wins\n4 - Defeats\n5 - Draw\n 6 - Hitted Goals\n7 - Missed Goals"));
                if (selector == null)
                    return;
                var team = GetTeam(UserPrompt("Enrter teams name"));

                switch (selector)
                {
                    case 1:
                        team.Name = UserPrompt("Enter teams name");
                        break;
                    case 2:
                        team.City = UserPrompt("Enter teams city");
                        break;
                    case 3:
                        team.WinCount = (int)ParseData<int>(UserPrompt("Enter number of wins")); 
                        break;
                    case 4:
                        team.DefCount = (int)ParseData<int>(UserPrompt("Enter number of defeats")); 
                        break;
                    case 5:
                        team.DrawCount = (int)ParseData<int>(UserPrompt("Enter number of draws")); 
                        break;
                    case 6:
                        team.HittedGoals = ParseData<int>(UserPrompt("Enter number of  hitted goals"));
                        break;
                    case 7:
                        team.MissedGoals = ParseData<int>(UserPrompt("Enter number of  missed goals"));
                        break;
                }

                context.Update(team);
                context.SaveChanges();

            }
        }
    }
}