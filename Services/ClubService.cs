using ClubAtlasAPI.Database;
using ClubAtlasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubAtlasAPI.Services;

public class ClubService : IClubService
{
    private readonly ClubAtlasDb _db;
    private readonly IGeoService _geoService;

    public ClubService(ClubAtlasDb db, IGeoService geoService)
    {
        _db = db;
        _geoService = geoService;
    }

    public async Task<IEnumerable<Club>> GetAllClubsAsync()
    {
        return await _db.Clubs.ToListAsync();
    }

    public async Task<IEnumerable<Club>> GetAllClubsWithActivitiesAsync()
    {
        return await _db.Clubs
            .Include(c => c.ActivityByClubs)
                .ThenInclude(ac => ac.Activity)
            .ToListAsync();
    }

    public async Task<Club?> GetClubByIdAsync(int id)
    {
        return await _db.Clubs.FindAsync(id);
    }

    public async Task<Club?> GetClubWithActivitiesByIdAsync(int id)
    {
        return await _db.Clubs
            .Include(c => c.ActivityByClubs)
                .ThenInclude(ac => ac.Activity)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Club> CreateClubAsync(Club club)
    {
        _db.Clubs.Add(club);
        await _db.SaveChangesAsync();
        return club;
    }

    public async Task<bool> UpdateClubAsync(int id, Club updatedClub)
    {
        var existingClub = await _db.Clubs.FindAsync(id);
        if (existingClub is null) return false;

        existingClub.Name = updatedClub.Name;
        existingClub.Stub = updatedClub.Stub;
        existingClub.Presentation = updatedClub.Presentation;
        existingClub.StartDate = updatedClub.StartDate;
        existingClub.Adress = updatedClub.Adress;
        existingClub.Phone = updatedClub.Phone;
        existingClub.Email = updatedClub.Email;
        existingClub.Contact = updatedClub.Contact;
        existingClub.InstagramUrl = updatedClub.InstagramUrl;
        existingClub.FacebookUrl = updatedClub.FacebookUrl;
        existingClub.TwitterUrl = updatedClub.TwitterUrl;
        existingClub.BlueskyUrl = updatedClub.BlueskyUrl;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteClubAsync(int id)
    {
        var club = await _db.Clubs.FindAsync(id);
        if (club is null) return false;

        _db.Clubs.Remove(club);
        await _db.SaveChangesAsync();
        return true;
    }

    // Get clubs within radius of a specific address
    public async Task<IEnumerable<Club>> GetClubsWithinRadiusAsync(string address, double radiusKm)
    {
        return await _geoService.GetClubsWithinRadiusAsync(address, radiusKm);
    }

    // Update a club's coordinates based on its address
    public async Task<bool> UpdateClubCoordinatesAsync(int id)
    {
        var club = await _db.Clubs.FindAsync(id);
        if (club is null) return false;

        return await _geoService.UpdateClubCoordinatesAsync(club);
    }
}