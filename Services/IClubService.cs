using ClubAtlasAPI.Models;

namespace ClubAtlasAPI.Services;

public interface IClubService
{
    Task<IEnumerable<Club>> GetAllClubsAsync();
    Task<Club?> GetClubByIdAsync(int id);
    Task<Club> CreateClubAsync(Club club);
    Task<bool> UpdateClubAsync(int id, Club updatedClub);
    Task<bool> DeleteClubAsync(int id);
}
