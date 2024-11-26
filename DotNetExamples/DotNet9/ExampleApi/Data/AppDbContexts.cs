using Microsoft.EntityFrameworkCore;
using Models;

namespace ExampleApi.Data
{
    public class AppDbContexts(DbContextOptions<AppDbContexts> options) : DbContext(options)
    {
        public DbSet<Provider> Providers => Set<Provider>();

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
        }
    }
}
