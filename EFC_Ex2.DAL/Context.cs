using EFC_Ex2.DAL.Moduls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFC_Ex2.DAL
{
    public class Context : DbContext
    {
        public DbSet<SoccerTeams> SoccerTeams { get; set; }

        public DbSet<Matches> Matches { get;set;}

        public DbSet<SoccerTeamComposition> SoccerTeamCmp {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(AppContext.BaseDirectory)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var connectionStr = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Matches>()
                .HasOne<SoccerTeams>(f => f.Team1)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Matches>()
                .HasOne<SoccerTeams>(f => f.Team2)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}