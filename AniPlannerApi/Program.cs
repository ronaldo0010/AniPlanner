using AnimeSeed;
using Contracts;
using Entities.Data;
using Microsoft.EntityFrameworkCore;
using Repository;

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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
        Console.WriteLine("==== APPLYING MIGRATIONS ====");
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        db.Database.Migrate(); // Creates and migrates database
        
        var tasks = new List<Task>();
        // TODO: check that this runs in batches
        await foreach (var mediaList in DataSeeding.ProcessDataAsync(db))
        {
            tasks.Add(db.AddRangeAsync(mediaList));
        }
        
        await Task.WhenAll(tasks);
        await db.SaveChangesAsync();
        

        Console.WriteLine("==== FINISHED MIGRATIONS ====");
    }
    catch(Exception ex)
    {
        var msg = $"================\nERROR APPLYING MIGRATION:\n{ex.Message}\n================";
        Console.WriteLine(msg);
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();