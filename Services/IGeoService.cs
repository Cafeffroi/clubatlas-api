using ClubAtlasAPI.Models;

namespace ClubAtlasAPI.Services;

public interface IGeoService
{
    // Calculate distance between two coordinates using the Haversine formula
    double CalculateDistance(double lat1, double lon1, double lat2, double lon2);

    // Find clubs within a certain radius of a location
    Task<IEnumerable<Club>> GetClubsWithinRadiusAsync(string address, double radiusKm);

    // Get coordinates for an address (geocoding)
    Task<(double latitude, double longitude)?> GetCoordinatesForAddressAsync(string address);

    // Update a club's coordinates based on its address
    Task<bool> UpdateClubCoordinatesAsync(Club club);
}