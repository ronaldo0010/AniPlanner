using System.Data.Common;
using AniPlannerApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Connection");

builder.Services
    .AddDbContext<DataContext>(options => options
        .UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using(var scope = app.Services.CreateScope())
{
    try
    {
        Console.WriteLine("DB migration started");
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        db.Database.Migrate(); // Creates and migrates database
        var isSeeded = await AnimeSeed.SeedData(db);
        if (!isSeeded) throw new Exception("Seed Data Failed");
    }
    catch(Exception ex)
    {
        Console.WriteLine($"================\n" +
                          $"ERROR APPLYING MIGRATION:" +
                          $"\n{ex.Message}\n" +
                          $"================");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();