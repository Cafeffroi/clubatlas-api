using ClubAtlasAPI.Models;

namespace ClubAtlasAPI.Services
{
    public interface IActivityByClubService
    {
        Task<ActivityByClub> CreateActivityByClubAsync(ActivityByClub activityByClub);
        Task<bool> DeleteActivityByClubAsync(int id);
    }
}
