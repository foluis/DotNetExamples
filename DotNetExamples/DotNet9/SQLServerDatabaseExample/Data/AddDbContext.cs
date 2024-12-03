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
                    Title = "Perrors el loco",
                    Platform = "Saul"
                },
                new()
                {
                    Id = 2,
                    Title = "Magico Mundo",
                    Platform = "Mario"
                },
                new()
                {
                    Id = 3,
                    Title = "Carpintero",
                    Platform = "Mario"
                }
            );
        }
    }
}
