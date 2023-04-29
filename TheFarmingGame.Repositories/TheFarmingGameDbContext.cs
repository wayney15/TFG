using Microsoft.EntityFrameworkCore;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public class TheFarmingGameDbContext : DbContext
    {
        public TheFarmingGameDbContext(DbContextOptions<TheFarmingGameDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Land> Land  { get; set; }
    }
}