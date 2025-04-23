using ClubAtlasAPI.Models;

namespace ClubAtlasAPI.Services;

public interface IActivityService
{
    Task<IEnumerable<Activity>> GetAllActivitiesAsync();
}
