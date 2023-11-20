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
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasOne<Team>(s => s.Team)
                .WithMany(g => g.Members)
                .HasForeignKey(s => s.TeamId)
                .IsRequired();
        }
    }
}
