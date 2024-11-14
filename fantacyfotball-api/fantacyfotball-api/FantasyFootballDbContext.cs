using fantacyfotball_api.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace fantacyfotball_api
{
    public class FantasyFootballDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Player> Players { get; init; }
        public DbSet<Team> Teams { get; init; }
        public DbSet<User> Users { get; init; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Player>()
            //  .ToCollection("Players");
        }
    }
}
