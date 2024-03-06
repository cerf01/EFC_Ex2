using EFC_Ex2.DAL.Moduls;
using Microsoft.EntityFrameworkCore;

namespace EFC_Ex2.DAL
{
    public class Repository
    {
        private Context _context;

        public Repository()
        {
            _context = new Context();
        }

        public void add(object entity)
        {

            _context.Add(entity);
            _context.SaveChanges();

        }

        public void delete(object entity)
        {

            _context.Remove(entity);
            _context.SaveChanges();

        }

        public void update(object entity)
        {

            _context.Update(entity);
            _context.SaveChanges();

        }

        public SoccerTeams GetTeam(string input)
        {

            var result = _context.SoccerTeams
                                 .Where(t => t.Name.ToLower() == input.ToLower())
                                 .First();
            return result;


        }

        public SoccerTeamComposition GetPlayer(string input)
        {

            var result = _context.SoccerTeamCmp
                                .Where(p => p.FullName.ToLower()
                                .Contains(input.ToLower()))
                                .First();
            return result;


        }

        public Matches GetMatch(DateTime input)
        {

            var result = _context.Matches
                                  .Where(m => m.DateOfMatch
                                  .Equals(input))
                                  .First();
            return result;

        }

        public DbSet<SoccerTeams> GetAllTeams()
        {
                var result = _context.SoccerTeams;
                return result;
           
        }

        public DbSet<Matches> GetAllMatches()
        {
                var result = _context.Matches;
                return result;
           
        }

        public DbSet<SoccerTeamComposition> GetAllPlaysers()
        {
            var result = _context.SoccerTeamCmp;
            return result;
        }







        //public DbSet<TEntity> GetAll(TEntity entity)
        //{

        //    var result = _context.Set<entity>;
        //    return result;

        //}

    }
}
