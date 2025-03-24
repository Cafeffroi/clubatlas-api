using ClubAtlasAPI.Database;
using ClubAtlasAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ClubDb>(opt => opt.UseInMemoryDatabase("ClubList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/clubitems", async (ClubDb db) =>
    await db.Clubs.ToListAsync());

app.MapGet("/clubitems/{id}", async (int id, ClubDb db) =>
    await db.Clubs.FindAsync(id)
        is Club club
            ? Results.Ok(club)
            : Results.NotFound());

app.MapPost("/clubitems", async (Club club, ClubDb db) =>
{
    db.Clubs.Add(club);
    await db.SaveChangesAsync();

    return Results.Created($"/clubitems/{club.Id}", club);
});

app.MapPut("/clubitems/{id}", async (int id, Club inputClub, ClubDb db) =>
{
    var club = await db.Clubs.FindAsync(id);
    if (club is null) return Results.NotFound();

    club.Name = inputClub.Name;
    club.Stub = inputClub.Stub;
    club.Presentation = inputClub.Presentation;
    club.StartDate = inputClub.StartDate;
    club.Adress = inputClub.Adress;
    club.Phone = inputClub.Phone;
    club.Email = inputClub.Email;
    club.Contact = inputClub.Contact;
    club.InstagramUrl = inputClub.InstagramUrl;
    club.FacebookUrl = inputClub.FacebookUrl;
    club.TwitterUrl = inputClub.TwitterUrl;
    club.BlueskyUrl = inputClub.BlueskyUrl;

    await db.SaveChangesAsync();
    return Results.NoContent();
});


app.MapDelete("/clubitems/{id}", async (int id, ClubDb db) =>
{
    if (await db.Clubs.FindAsync(id) is Club club)
    {
        db.Clubs.Remove(club);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

await app.RunAsync();