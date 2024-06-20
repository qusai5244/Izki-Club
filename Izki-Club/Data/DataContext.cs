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
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentTeam> TournamentTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Member>()
                .HasQueryFilter(m => !m.IsDeleted);

            modelBuilder.Entity<Team>()
                .HasOne(s => s.Organization)
                .WithMany(g => g.Teams)
                .HasForeignKey(s => s.OrganizationId)
                .IsRequired();

            
            modelBuilder.Entity<Tournament>()
                .HasOne(s => s.Organization)
                .WithMany(g => g.Tournaments)
                .HasForeignKey(s => s.OrganizationId)
                .IsRequired();            
            
            modelBuilder.Entity<TournamentTeam>()
                .HasOne(s => s.Tournament)
                .WithMany(g => g.TournamentTeams)
                .HasForeignKey(s => s.TournamentId)
                .IsRequired();            
            
            modelBuilder.Entity<TournamentTeam>()
                .HasOne(s => s.Team)
                .WithMany(g => g.TournamentTeams)
                .HasForeignKey(s => s.TeamId)
                .IsRequired();
        }
    }
}