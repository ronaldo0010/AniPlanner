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
        AnimeSeed.SeedData(db);

    }
    catch(Exception ex)
    {
        Console.WriteLine($"Error running migration {ex.Message}");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();