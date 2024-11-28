using Microsoft.EntityFrameworkCore;
using Models;

namespace SQLServerDatabaseExample.Data
{
    public class AppDbContexts(DbContextOptions<AppDbContexts> options) : DbContext(options)
    {
        public DbSet<VideoGame> VideoGames => Set<VideoGame>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>().HasData(
                new()
                {
                    Id = 1,
                    Name = "Perrors el loco",
                    Description = "Saul"
                },
                new()
                {
                    Id = 2,
                    Name = "Magico Mundo",
                    Description = "Mario"
                },
                new()
                {
                    Id = 3,
                    Name = "Carpintero",
                    Description = "Mario"
                }
            );
        }
    }
}
