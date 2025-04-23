using ClubAtlasAPI.Database;
using ClubAtlasAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace ClubAtlasAPI.Services;

public class GeoService : IGeoService
{
    private readonly ClubAtlasDb _db;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private const double EarthRadiusKm = 6371; // Earth radius in kilometers

    public GeoService(ClubAtlasDb db, HttpClient httpClient, IConfiguration configuration)
    {
        _db = db;
        _httpClient = httpClient;
        _configuration = configuration;
    }

    // Calculate distance between two points using the Haversine formula
    public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return EarthRadiusKm * c;
    }

    private static double ToRadians(double degrees) => degrees * Math.PI / 180;

    // Get clubs within a certain radius
    public async Task<IEnumerable<Club>> GetClubsWithinRadiusAsync(string address, double radiusKm)
    {
        var coordinates = await GetCoordinatesForAddressAsync(address);
        if (!coordinates.HasValue)
            return Enumerable.Empty<Club>();

        var (latitude, longitude) = coordinates.Value;

        // Get all clubs with coordinates
        var allClubs = await _db.Clubs
            .Where(c => c.Latitude.HasValue && c.Longitude.HasValue)
            .ToListAsync();

        // Filter clubs by distance
        return allClubs.Where(club =>
            CalculateDistance(
                latitude,
                longitude,
                club.Latitude!.Value,
                club.Longitude!.Value) <= radiusKm);
    }

    // Get coordinates for an address using a geocoding service
    public async Task<(double latitude, double longitude)?> GetCoordinatesForAddressAsync(string address)
    {
        // For a real implementation, you'd use a geocoding service like Google Maps, Mapbox, etc.
        // This is a simplified mock implementation
        // In a real app, add API key to configuration and use it in requests

        try
        {
            // For real implementation, replace with actual API call
            // Example using OpenStreetMap Nominatim API (note: has strict usage limits)
            var encodedAddress = Uri.EscapeDataString(address);
            var response = await _httpClient.GetAsync(
                $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&limit=1");

            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadFromJsonAsync<List<GeocodingResult>>();
                if (results != null && results.Count > 0)
                {
                    return (double.Parse(results[0].Lat), double.Parse(results[0].Lon));
                }
            }

            // Return a dummy result for testing
            // In a real app, return null or throw exception
            return (48.8566, 2.3522); // Paris coordinates for demo
        }
        catch (Exception)
        {
            // Log exception
            return null;
        }
    }

    // Update a club's coordinates based on its address
    public async Task<bool> UpdateClubCoordinatesAsync(Club club)
    {
        if (string.IsNullOrWhiteSpace(club.Adress))
            return false;

        var coordinates = await GetCoordinatesForAddressAsync(club.Adress);
        if (!coordinates.HasValue)
            return false;

        var (latitude, longitude) = coordinates.Value;
        club.Latitude = latitude;
        club.Longitude = longitude;

        await _db.SaveChangesAsync();
        return true;
    }

    // Helper class for deserializing geocoding results
    private class GeocodingResult
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; } = string.Empty;

        [JsonPropertyName("lon")]
        public string Lon { get; set; } = string.Empty;
    }
}