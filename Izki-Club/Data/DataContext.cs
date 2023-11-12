using Izki_Club.Models;
using Microsoft.EntityFrameworkCore;
namespace Izki_Club.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Coach> Coachs { get; set; }
    }
}
