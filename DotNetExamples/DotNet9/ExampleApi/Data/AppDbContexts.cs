using Microsoft.EntityFrameworkCore;
using Models;

namespace ExampleApi.Data
{
    public class AppDbContexts(DbContextOptions<AppDbContexts> options) : DbContext(options)
    {
        public DbSet<Provider> Providers => Set<Provider>();
        public DbSet<VideoGame> VideoGames => Set<VideoGame>();
        public DbSet<VideoGameDetails> VideoGameDetails => Set<VideoGameDetails>();
        public DbSet<Publisher> Publishers => Set<Publisher>();
        public DbSet<Developer> Developers => Set<Developer>();
        public DbSet<Genre> Genres => Set<Genre>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Provider>().HasData(
                new()
                {
                    Id = 1,
                    Name = "Perrors el loco",
                    ContactName = "Saul",
                    Phone = "1234"
                },
                new()
                {
                    Id = 2,
                    Name = "Magico Mundo",
                    ContactName = "Mario",
                    Phone = "23425"
                },
                new()
                {
                    Id = 3,
                    Name = "Carpintero",
                    ContactName = "Mario",
                    Phone = "23425"
                }
            );

            modelBuilder.Entity<VideoGame>().HasData(
               new()
               {
                   Id = 1,
                   Title = "Zelda",                 
                   Platform = "Nintendo"
               },
               new()
               {
                   Id = 2,
                   Title = "Mario Kart",                   
                   Platform = "Nintendo"
               },
               new()
               {
                   Id = 3,
                   Title = "Bomber Man",
                   Platform = "Nintendo"
               }
           );
        }        
    }
}
