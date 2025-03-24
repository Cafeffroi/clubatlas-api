using ClubAtlasAPI.Database;
using ClubAtlasAPI.Endpoints;
using ClubAtlasAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddDbContext<ClubDb>(opt => opt.UseInMemoryDatabase("ClubList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IClubService, ClubService>(); // Register the service

var app = builder.Build();

// Register API endpoints
app.MapClubEndpoints();

await app.RunAsync();
