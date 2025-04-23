using ClubAtlasAPI.Models;

namespace ClubAtlasAPI.Services;

public interface IClubService
{
    Task<IEnumerable<Club>> GetAllClubsAsync();
    Task<IEnumerable<Club>> GetAllClubsWithActivitiesAsync();
    Task<Club?> GetClubByIdAsync(int id);
    Task<Club?> GetClubWithActivitiesByIdAsync(int id);
    Task<Club> CreateClubAsync(Club club);
    Task<bool> UpdateClubAsync(int id, Club updatedClub);
    Task<bool> DeleteClubAsync(int id);
    Task<IEnumerable<Club>> GetClubsWithinRadiusAsync(string address, double radiusKm);
    Task<bool> UpdateClubCoordinatesAsync(int id);
}