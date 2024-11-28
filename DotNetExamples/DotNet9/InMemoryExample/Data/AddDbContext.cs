using Microsoft.EntityFrameworkCore;
using Models;

namespace InMemoryExample.Data
{
    public class AppDbContexts(DbContextOptions<AppDbContexts> options) : DbContext(options)
    {
        public DbSet<VideoGame> VideoGames => Set<VideoGame>();
    }
}
