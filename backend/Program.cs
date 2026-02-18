using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Repository;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration

builder.Services.Configure<OpenWeatherSettings>(
    builder.Configuration.GetSection("OpenWeather"));

// Database

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ClimaDb"));

// Dependency Injection

builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<OpenWeatherClient>();

// Infrastructure

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware

app.UseCors("AllowAngular");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();