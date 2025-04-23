using ClubAtlasAPI.Models;
using ClubAtlasAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClubAtlasAPI.Endpoints;

public static class ClubEndpoints
{
    public static void MapClubEndpoints(this WebApplication app)
    {
        var clubs = app.MapGroup("/clubs");

        clubs.MapGet("/", GetAllClubs);
        clubs.MapGet("/with-activities", GetAllClubsWithActivities);
        clubs.MapGet("/{id}", GetClub);
        clubs.MapGet("/{id}/with-activities", GetClubWithActivities);
        clubs.MapPost("/", CreateClub);
        clubs.MapPut("/{id}", UpdateClub);
        clubs.MapDelete("/{id}", DeleteClub);
        clubs.MapGet("/nearby", GetClubsNearby);
        clubs.MapPut("/{id}/update-coordinates", UpdateClubCoordinates);
    }

    static async Task<Ok<IEnumerable<Club>>> GetAllClubs(IClubService service)
    {
        return TypedResults.Ok(await service.GetAllClubsAsync());
    }

    static async Task<Ok<IEnumerable<Club>>> GetAllClubsWithActivities(IClubService service)
    {
        return TypedResults.Ok(await service.GetAllClubsWithActivitiesAsync());
    }

    static async Task<Results<Ok<Club>, NotFound>> GetClub(int id, IClubService service)
    {
        var club = await service.GetClubByIdAsync(id);
        return club is not null ? TypedResults.Ok(club) : TypedResults.NotFound();
    }

    static async Task<Results<Ok<Club>, NotFound>> GetClubWithActivities(int id, IClubService service)
    {
        var club = await service.GetClubWithActivitiesByIdAsync(id);
        return club is not null ? TypedResults.Ok(club) : TypedResults.NotFound();
    }

    static async Task<Created<Club>> CreateClub(Club club, IClubService service)
    {
        var newClub = await service.CreateClubAsync(club);
        return TypedResults.Created($"/clubs/{newClub.Id}", newClub);
    }

    static async Task<Results<NoContent, NotFound>> UpdateClub(int id, Club updatedClub, IClubService service)
    {
        return await service.UpdateClubAsync(id, updatedClub)
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }

    static async Task<Results<NoContent, NotFound>> DeleteClub(int id, IClubService service)
    {
        return await service.DeleteClubAsync(id)
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }

    // Get clubs within a certain radius of an address
    static async Task<Ok<IEnumerable<Club>>> GetClubsNearby(
        string address,
        double radiusKm,
        IClubService service)
    {
        var clubs = await service.GetClubsWithinRadiusAsync(address, radiusKm);
        return TypedResults.Ok(clubs);
    }

    // Force update of a club's coordinates based on its address
    static async Task<Results<NoContent, NotFound>> UpdateClubCoordinates(
        int id,
        IClubService service)
    {
        return await service.UpdateClubCoordinatesAsync(id)
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }
}