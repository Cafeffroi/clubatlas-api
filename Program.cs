using ClubAtlasAPI.Database;
using ClubAtlasAPI.Endpoints;
using ClubAtlasAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddDbContext<ClubAtlasDb>(opt => opt.UseInMemoryDatabase("ClubList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IActivityByClubService, ActivityByClubService>();
builder.Services.AddScoped<IGeoService, GeoService>();

var app = builder.Build();

// Register API endpoints
app.MapClubEndpoints();
app.MapActivityEndpoints();
app.MapActivityByClubEndpoints();

await app.RunAsync();
