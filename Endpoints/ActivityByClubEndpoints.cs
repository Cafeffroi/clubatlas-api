using ClubAtlasAPI.Models;
using ClubAtlasAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClubAtlasAPI.Endpoints;

public static class ActivityByClubEndpoints
{
    public static void MapActivityByClubEndpoints(this WebApplication app)
    {
        var activityByClubs = app.MapGroup("/activity-by-clubs");

        activityByClubs.MapPost("/", CreateActivityByClub);
        activityByClubs.MapDelete("/{id}", DeleteActivityByClub);
    }

    static async Task<Created<ActivityByClub>> CreateActivityByClub(ActivityByClub activityByClub, IActivityByClubService service)
    {
        var newActivityByClub = await service.CreateActivityByClubAsync(activityByClub);
        return TypedResults.Created($"/activity-by-clubs/{newActivityByClub.Id}", newActivityByClub);
    }

    static async Task<Results<NoContent, NotFound>> DeleteActivityByClub(int id, IActivityByClubService service)
    {
        return await service.DeleteActivityByClubAsync(id)
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }
}