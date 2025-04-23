using ClubAtlasAPI.Database;
using ClubAtlasAPI.Models;

namespace ClubAtlasAPI.Services;

public class ActivityByClubService : IActivityByClubService
{
    private readonly ClubAtlasDb _db;

    public ActivityByClubService(ClubAtlasDb db)
    {
        _db = db;
    }

    public async Task<ActivityByClub> CreateActivityByClubAsync(ActivityByClub activityByClub)
    {
        _db.ActivityByClubs.Add(activityByClub);
        await _db.SaveChangesAsync();
        return activityByClub;
    }

    public async Task<bool> DeleteActivityByClubAsync(int id)
    {
        var activityByClub = await _db.ActivityByClubs.FindAsync(id);
        if (activityByClub is null) return false;

        _db.ActivityByClubs.Remove(activityByClub);
        await _db.SaveChangesAsync();
        return true;
    }
}