using EFC_Ex2.DAL.Moduls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFC_Ex2.DAL
{
    public class Context : DbContext
    {
        public DbSet<SoccerTeams> SoccerTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(AppContext.BaseDirectory)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var connectionStr = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionStr);
        }
    }
}