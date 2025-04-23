using ClubAtlasAPI.Database;
using ClubAtlasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubAtlasAPI.Services;

public class ActivityService : IActivityService
{
    private readonly ClubAtlasDb _db;

    public ActivityService(ClubAtlasDb db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
    {
        return await _db.Activities.ToListAsync();
    }
}
