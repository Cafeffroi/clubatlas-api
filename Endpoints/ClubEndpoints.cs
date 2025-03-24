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
        clubs.MapGet("/{id}", GetClub);
        clubs.MapPost("/", CreateClub);
        clubs.MapPut("/{id}", UpdateClub);
        clubs.MapDelete("/{id}", DeleteClub);
    }

    static async Task<Ok<IEnumerable<Club>>> GetAllClubs(IClubService service)
    {
        return TypedResults.Ok(await service.GetAllClubsAsync());
    }

    static async Task<Results<Ok<Club>, NotFound>> GetClub(int id, IClubService service)
    {
        var club = await service.GetClubByIdAsync(id);
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
}
