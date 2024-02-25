using EFC_Ex2.DAL.Moduls;
using Microsoft.EntityFrameworkCore;

namespace EFC_Ex2.DAL
{
    public class Context : DbContext
    {
        private string _connectionStr => "Data Source=DESKTOP-BRQ9LQE\\SQLEXPRESS;Initial Catalog=Sport;Integrated Security=True;Connect Timeout=30;";

        public DbSet<SoccerTeams> SoccerTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStr); ;
        }
    }
}