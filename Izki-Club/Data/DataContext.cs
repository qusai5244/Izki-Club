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
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne<Team>(s => s.Team)
                .WithMany(g => g.Persons)
                .HasForeignKey(s => s.TeamId)
                .IsRequired();
        }
    }
}
