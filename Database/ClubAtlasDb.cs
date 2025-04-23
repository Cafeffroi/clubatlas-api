using ClubAtlasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubAtlasAPI.Database
{
    public class ClubAtlasDb : DbContext
    {
        public ClubAtlasDb(DbContextOptions<ClubAtlasDb> options)
            : base(options) { }

        public DbSet<Club> Clubs => Set<Club>();
        public DbSet<Activity> Activities => Set<Activity>();
        public DbSet<ActivityByClub> ActivityByClubs => Set<ActivityByClub>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships
            modelBuilder.Entity<ActivityByClub>()
                .HasOne(ac => ac.Club)
                .WithMany(c => c.ActivityByClubs)
                .HasForeignKey(ac => ac.ClubId);

            modelBuilder.Entity<ActivityByClub>()
                .HasOne(ac => ac.Activity)
                .WithMany(a => a.ActivityByClubs)
                .HasForeignKey(ac => ac.ActivityId);
        }
    }
}