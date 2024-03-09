using EFC_Ex2.DAL;
using EFC_Ex2.DAL.Moduls;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var teams = new List<SoccerTeams>()
                {   new SoccerTeams()
                    {
                        Name = "Team1",

                        City = "City17",

                        WinCount = 12,

                        DefCount = 8,

                        DrawCount = 3,

                        HittedGoals = 14,

                        MissedGoals = 11
                    },
                    new SoccerTeams()
                    {
                        Name = "14Gorilaz",

                        City = "NewBrock",

                        WinCount = 12,

                        DefCount = 8,

                        DrawCount = 3,

                        HittedGoals = 14,

                        MissedGoals = 11
                    },
                };

            var match = new Matches()
            {
                Team1Id = 0,
                Team2Id = 1,
                HittedGoalsByTeam1 = 10,
                HittedGoalsByTeam2 = 7,
                DateOfMatch = new DateTime(2023, 12, 12),
                Winner = "14Gorilaz"
            };

            var player = new SoccerTeamComposition()
            {
                FullName = "Team1Player",
                Country = "Coyntry17",
                Position = "Pos5",
                TeamId = teams[0].Id
            };

            var option = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "Sport")
                .Options;

            /*   using (var context = new Context(option))
               {           
                   context.Matches.Add(match);
                   context.SoccerTeams.AddRange(teams);
                   context.SaveChanges();
               }

               using (var context = new Context(option)) 
               {
                   var cTeams = context.SoccerTeams.ToList();
                   Assert.Equal(2, context.SoccerTeams.Count());
                   for (int i = 0; i < cTeams.Count();i++)
                   {
                       Assert.Equal(teams[i].Name, cTeams[i].Name);
                       Assert.Equal(teams[i].City, cTeams[i].City);
                       Assert.Equal(teams[i].WinCount, cTeams[i].WinCount);
                       Assert.Equal(teams[i].DefCount, cTeams[i].DefCount);
                       Assert.Equal(teams[i].DrawCount, cTeams[i].DrawCount);
                       Assert.Equal(teams[i].HittedGoals, cTeams[i].HittedGoals);
                       Assert.Equal(teams[i].MissedGoals, cTeams[i].MissedGoals);
                   }


               }

               using (var context = new Context(option))
               {
                   var cMatch = context.Matches.ToList().First();
                   Assert.Equal(1, context.Matches.Count());
                   Assert.Equal(match.Team1Id, cMatch.Team1Id);
                   Assert.Equal(match.Team2Id, cMatch.Team2Id);
                   Assert.Equal(match.DateOfMatch, cMatch.DateOfMatch);
                   Assert.Equal(match.HittedGoalsByTeam1, cMatch.HittedGoalsByTeam1);
                   Assert.Equal(match.HittedGoalsByTeam2, cMatch.HittedGoalsByTeam2);
               }

               using(var context = new Context(option)) 
               {
                   var cPlayer = context.SoccerTeamCmp.ToList().First();
                   Assert.Equal(1, context.Matches.Count());
                   Assert.Equal(player.TeamId, cPlayer.TeamId);
                   Assert.Equal(player.FullName, cPlayer.FullName);
                   Assert.Equal(player.Country, cPlayer.Country);
                   Assert.Equal(player.Position, cPlayer.Position);
               }*/


            var _teamRepository = new Repository<SoccerTeams>(option);
            var _playerRepository = new Repository<SoccerTeamComposition>(option);
            var _matchesRepository = new Repository<Matches>(option);

            _teamRepository.addRange(teams);
            _matchesRepository.add(match);
            _playerRepository.add(player);

            var cTeams = _teamRepository.GetAll().ToList();
            Assert.Equal(2, cTeams.Count());
            for (int i = 0; i < cTeams.Count(); i++)
            {
                Assert.Equal(teams[i].Name, cTeams[i].Name);
                Assert.Equal(teams[i].City, cTeams[i].City);
                Assert.Equal(teams[i].WinCount, cTeams[i].WinCount);
                Assert.Equal(teams[i].DefCount, cTeams[i].DefCount);
                Assert.Equal(teams[i].DrawCount, cTeams[i].DrawCount);
                Assert.Equal(teams[i].HittedGoals, cTeams[i].HittedGoals);
                Assert.Equal(teams[i].MissedGoals, cTeams[i].MissedGoals);
            }

            var cMatch = _matchesRepository.Get(match.Id);
            Assert.Equal(1, _matchesRepository.GetAll().Count());

            Assert.Equal(match.Team1Id, cMatch.Team1Id);
            Assert.Equal(match.Team2Id, cMatch.Team2Id);
            Assert.Equal(match.DateOfMatch, cMatch.DateOfMatch);
            Assert.Equal(match.HittedGoalsByTeam1, cMatch.HittedGoalsByTeam1);



            var cPlayer = _playerRepository.Get(player.Id);
            Assert.Equal(1, _playerRepository.GetAll().Count());
            Assert.Equal(player.TeamId, cPlayer.TeamId);
            Assert.Equal(player.FullName, cPlayer.FullName);
            Assert.Equal(player.Country, cPlayer.Country);
            Assert.Equal(player.Position, cPlayer.Position);


        }
    }
}