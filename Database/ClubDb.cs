using ClubAtlasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubAtlasAPI.Database
{
    public class ClubDb : DbContext
    {
        public ClubDb(DbContextOptions<ClubDb> options)
            : base(options) { }

        public DbSet<Club> Clubs => Set<Club>();
    }
}