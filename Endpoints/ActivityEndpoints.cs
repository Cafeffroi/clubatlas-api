using ClubAtlasAPI.Models;
using ClubAtlasAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClubAtlasAPI.Endpoints;

public static class ActivityEndpoints
{
    public static void MapActivityEndpoints(this WebApplication app)
    {
        var activities = app.MapGroup("/activities");

        activities.MapGet("/", GetAllActivities);
    }

    static async Task<Ok<IEnumerable<Activity>>> GetAllActivities(IActivityService service)
    {
        return TypedResults.Ok(await service.GetAllActivitiesAsync());
    }
}
