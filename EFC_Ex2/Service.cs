using EFC_Ex2.DAL;
using EFC_Ex2.DAL.Moduls;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace EFC_Ex2
{
    public class Service
    {

        private Repository<SoccerTeams> _teamRepository;
        private Repository<SoccerTeamComposition> _PlayerRepository;
        private Repository<Matches> _matchesRepository;
        public Service()
        {
            _teamRepository = new Repository<SoccerTeams>();
            _PlayerRepository = new Repository<SoccerTeamComposition>();
            _matchesRepository = new Repository<Matches>();
        }

        private void ShowTeams()
        {
 
                var teamsInfo = _teamRepository.GetAll();
                Console.WriteLine("Team |  City | Wins | Defeats | Draw | Hitted Goals | Missed Goals");
                foreach (var team in teamsInfo)
                {
                    Console.WriteLine($"{team.Name} | {team.City} | {team.WinCount} | {team.DefCount} | {team.DrawCount} | {team.HittedGoals} | {team.MissedGoals}");
                }
           
        }

        private void ShowMatches()
        {

            var matchs = _matchesRepository
                .GetAll()
                .Include(m => m.Team1)
                .Include(m => m.Team2);
            
            Console.WriteLine("Date of match | Team 1 | Team 2 | Hitted goals by Team 1 | Hitted goals by Team 2 | Winner ");
            foreach (var match in matchs)
                Console.WriteLine($"{match.DateOfMatch} | { match.Team1.Name} | {match.Team2.Name} | {match.HittedGoalsByTeam1} | {match.HittedGoalsByTeam2} | ");

        }

        public void ShowData()
        {
            string tableName = UserPrompt("Enter table");
            switch (tableName.ToLower())
            {
                case "t":
                case "teams":
                    ShowTeams();
                    break;
                case "m":
                case "matches":
                    ShowMatches();
                    break;
                default:
                    {
                        Console.WriteLine("wrong input!");
                        return;
                    }
            }

        }

        public void ShowMaxThings(int stage)
        {

            var teamsInfo = _teamRepository.GetAll();

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

        private Dictionary<string, int> CalculPoints() 
        {
            var TemasPoints = new Dictionary<string, int>();

            var teams = _teamRepository.GetAll().ToList();

            int points = 0;
            for (int i = 0; i < teams.Count; i++)
            {
                points = (teams[i].WinCount * 3) + teams[i].DrawCount;
                TemasPoints.Add(teams[i].Name, points);
            }
            return TemasPoints;
        }

        public void ShowBestOfTeams(string stage) 
        {
            switch (stage) 
            {
                case "1":
                    {
                        var teams = _teamRepository.GetAll().OrderByDescending(t => t.HittedGoals).ToList();
                        for (int i  = 0; i <3; i++)
                        {
                            if (i >= teams.Count)
                                break;
                            Console.WriteLine(teams[i].Name+" "+ teams[i].HittedGoals);
                         
                        }
                        
                    }
                    break;

                    case"2": 
                    {
                        var item = _teamRepository.GetAll().OrderByDescending(t => t.HittedGoals).First();

                            Console.WriteLine(item.Name + " " + item.HittedGoals);

                    }
                    break;

                case "3":
                    {
                        var teams = _teamRepository.GetAll().OrderBy(t => t.MissedGoals).ToList();

                        for (int i = 0; i < 3; i++)
                        {
                            if (i >= teams.Count)
                                break;
                            Console.WriteLine(teams[i].Name + " " + teams[i].MissedGoals);

                        }

                    }
                    break;

                case "4":
                    {
                        var team = _teamRepository.GetAll().OrderBy(t => t.MissedGoals).First();

                        Console.WriteLine(team.Name + " " + team.MissedGoals);

                    }
                    break;

                    case "5":
                    {

                        var ordered = CalculPoints().OrderByDescending(p => p.Value).ToList();
                        for (int i = 0; i < 3; i++)
                        {
                            if (i >= ordered.Count)
                                break;
                            Console.WriteLine(ordered[i].Key+" "+ ordered[i].Value);
                        }

                    }
                    break;

                case "6":
                    {

                        var ordered = CalculPoints().OrderByDescending(p => p.Value).First();
             
                            Console.WriteLine(ordered.Key + " " + ordered.Value);
                        

                    }
                    break;

                case "7":
                    {

                        var ordered = CalculPoints().OrderBy(p => p.Value).ToList();
                        for (int i = 0; i < 3; i++)
                        {
                            if (i >= ordered.Count)
                                break;
                            Console.WriteLine(ordered[i].Key + " " + ordered[i].Value);
                        }

                    }
                    break;

                case "8":
                    {

                        var ordered = CalculPoints().OrderBy(p => p.Value).First();

                        Console.WriteLine(ordered.Key + " " + ordered.Value);


                    }
                    break;
            }
        }

        private void find1(string input)
        {

            var teamsInfo = _teamRepository.GetAll();

            var output = teamsInfo
                        .Where(e => e.Name.ToLower()
                        .Contains(input.ToLower()));

            Console.WriteLine("Team |  City | Wins | Defeats | Draw | Hitted Goals | Missed Goals");
            foreach (var item in output)
                Console.WriteLine($"{item.Name} | {item.City} | {item.WinCount} | {item.DefCount} | {item.DrawCount} | {item.HittedGoals} | {item.MissedGoals}");

        }
        private void find2(string input)
        {

            var matchs = _matchesRepository.GetAll().ToList();
 


                 var output = matchs.Where(e => e.DateOfMatch == ParseData<DateTime>(input));

            Console.WriteLine("Date of match | Team 1 | Team 2 | Hitted goals by Team 1 | Hitted goals by Team 2 | Winner ");
            foreach (var match in matchs)
                Console.WriteLine($"{match.DateOfMatch} | {match.Team1.Name} | {match.Team2.Name} | {match.HittedGoalsByTeam1} | {match.HittedGoalsByTeam2} | ");


        }

        private void find3(string input)
        {
            var matchs = _matchesRepository
                           .GetAll()
                           .ToList();

            var output = matchs.Where(e => e.DateOfMatch == ParseData<DateTime>(input)).Select(e => e.Winner);
            foreach (var match in matchs)
                Console.WriteLine($"{match.Winner}");

        }

        public void find4(string input)
        {
            var matchs = _matchesRepository.GetAll().ToList();

            var output = matchs.Where(e => e.Team1.Name == input || e.Team2.Name == input);

            Console.WriteLine("Date of match | Team 1 | Team 2 | Hitted goals by Team 1 | Hitted goals by Team 2 | Winner ");
            foreach (var match in matchs)
                Console.WriteLine($"{match.DateOfMatch} | {match.Team1.Name} | {match.Team2.Name} | {match.HittedGoalsByTeam1} | {match.HittedGoalsByTeam2} | ");



        }

        public void FindInfo(int stage)
        {
            switch (stage)
            {
                case 0:
                    find1(UserPrompt("Enter teams name/city"));
                    break;
                case 1:
                    find2(UserPrompt("Enter date"));
                    break;
                case 2:
                    find3(UserPrompt("Enter date"));
                    break;
                case 3:


                    break;
            }
        }  

        private T? ParseData<T>(string str) where T : struct
        {
            var type = typeof(T);
            if (type == typeof(int) && int.TryParse(str, out var intVal))
                return (T)(object)intVal;
            if (type == typeof(DateTime) && DateTime.TryParse(str, out var dateVal))
                return (T)(object)dateVal;
            return null;
        }

        private string UserPrompt(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        private SoccerTeams MapSoccerTeam()
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

        private SoccerTeamComposition MapPlayer()
        {
            var palayer = new SoccerTeamComposition()
            {
                FullName = UserPrompt("Enter plaeyrs fullname"),
                Country = UserPrompt("Enter players country"),
                Team = _teamRepository.Get(_teamRepository
                                    .GetAll()
                                    .Where(t => t.Name.ToLower() == UserPrompt("enter name").ToLower())
                                    .Select(t => t.Id)
                                    .First()),
                Position = UserPrompt("Enter players position")
            };
            return palayer;
        }

        private Matches MapMatch()
        {
            var match = new Matches()
            {
                Team1Id = _teamRepository.Get(_teamRepository
                                     .GetAll()
                                     .Where(t => t.Name.ToLower() == UserPrompt("enter name").ToLower())
                                     .Select(t => t.Id)
                                     .First()).Id,

                Team2Id = _teamRepository.Get(_teamRepository
                                     .GetAll()
                                     .Where(t => t.Name.ToLower() == UserPrompt("enter name").ToLower())
                                     .Select(t => t.Id)
                                     .First()).Id,

                DateOfMatch = (DateTime)ParseData<DateTime>(UserPrompt("Enter date of this match")),
                HittedGoalsByTeam1 = (int)ParseData<int>(UserPrompt("Enter hitted goals by team 1")),
                HittedGoalsByTeam2 = (int)ParseData<int>(UserPrompt("Enter hitted goals by team 2")),
                Winner = UserPrompt("Enter winner of this match"),
            };

            return match;
        }


        private SoccerTeams? UPDTeam(string prompt)
        {

            var team = _teamRepository.Get(_teamRepository
                                     .GetAll()
                                     .Where(t => t.Name.ToLower() == UserPrompt("enter name").ToLower())
                                     .Select(t => t.Id)
                                     .First());
            var selector = ParseData<int>(UserPrompt("Select field to update:\n 1 - Name\n2 - City\n3 - Wins\n4 - Defeats\n5 - Draw\n 6 - Hitted Goals\n7 - Missed Goals"));

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
                default: return null;
            }
            return team;
        }

       

        public void UpdateData()
        {
            Console.WriteLine("Select table to update: teams; matches");

            string tableName = UserPrompt("Enter table").ToLower();

            switch (tableName)
            {
                case "teams":
                    {
                        var team = _teamRepository.Get(_teamRepository
                                     .GetAll()
                                     .Where(t => t.Name.ToLower() == UserPrompt("enter name").ToLower())
                                     .Select(t => t.Id).First());
                        _teamRepository.update(team.Id, team);
                    }
                    break;
                case "matchs":
                    {
                        var match = _matchesRepository.Get(_matchesRepository
                                        .GetAll()
                                     .Where(t => t.DateOfMatch == ParseData<DateTime>(UserPrompt("enter match date"))).First().Id);
                        _matchesRepository.update(match.Id, match);
                    }
                    break;
                default: return;
            }
        }

        public void AddData()
        {
            Console.WriteLine("Select table to add: teams; matches");
            string tableName = UserPrompt("Enter table").ToLower();
            switch (tableName)
            {
                case "t":
                case "team":
                    {
                        _teamRepository.add(MapSoccerTeam());
                    }
                    break;
                case "m":
                case "match":
                    {

                        _matchesRepository.add(MapMatch());
                    }
                    break;
                case "p":
                case "player":
                    {
                        _PlayerRepository.add(MapPlayer());
                    }
                    break;
                default:
                    {
                        Console.WriteLine("wrong input!");
                        return;
                    }
            }
        }

        public void DeleteData()
        {
            Console.WriteLine("Select table to delete: teams; matches");

            string tableName = UserPrompt("Enter table").ToLower();

            switch (tableName)
            {
                case "team":
                    {
                        var team = _teamRepository.Get(_teamRepository
                                    .GetAll()
                                    .Where(t => t.Name.ToLower() == UserPrompt("enter name").ToLower())
                                    .Select(t => t.Id).First());
                        _teamRepository.delete(team.Id);
                    }
                    break;
                case "macth":
                    {
                        var match = _matchesRepository.Get(_matchesRepository
                                      .GetAll()
                                   .Where(t => t.DateOfMatch == ParseData<DateTime>(UserPrompt("enter match date")))
                                   .First().Id);
                        _matchesRepository.delete(match.Id);
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
}
