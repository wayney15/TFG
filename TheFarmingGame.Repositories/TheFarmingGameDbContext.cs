using Microsoft.EntityFrameworkCore;
using System.Xml;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public class TheFarmingGameDbContext : DbContext
    {
        public TheFarmingGameDbContext(DbContextOptions<TheFarmingGameDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Land> Lands  { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Alias)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(e => e.UserName)
                .IsUnique();
        }
    }
}