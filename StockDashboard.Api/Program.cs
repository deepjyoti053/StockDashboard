using Microsoft.EntityFrameworkCore;
using StockDashboard.Api.Data;
using StockDashboard.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Application Services
builder.Services.AddScoped<StockIngestionService>();
builder.Services.AddScoped<StockQueryService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Auto-migrate and Seed on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    try {
        var ingestion = scope.ServiceProvider.GetRequiredService<StockIngestionService>();
        
        // Debugging Paths
        var cwd = Directory.GetCurrentDirectory();
        Console.WriteLine($"[Seeding] Current Working Directory: {cwd}");
        
        var infyPath = Path.Combine(cwd, "Data", "INFY.csv");
        var tcsPath = Path.Combine(cwd, "Data", "TCS.csv");

        Console.WriteLine($"[Seeding] Looking for INFY at: {infyPath}");
        if (File.Exists(infyPath)) 
        {
            Console.WriteLine("[Seeding] INFY.csv found. Importing...");
            await ingestion.ImportFromCsvAsync("INFY", "Infosys Ltd", infyPath);
        }
        else
        {
            Console.WriteLine("[Seeding] ERROR: INFY.csv NOT FOUND.");
        }

        if (File.Exists(tcsPath))
        {
             Console.WriteLine("[Seeding] TCS.csv found. Importing...");
            await ingestion.ImportFromCsvAsync("TCS", "Tata Consultancy Services", tcsPath);
        }
        else
        {
            Console.WriteLine("[Seeding] ERROR: TCS.csv NOT FOUND.");
        }

    } catch (Exception ex) {
        Console.WriteLine($"[Seeding] FATAL ERROR: {ex.Message}");
        Console.WriteLine(ex.StackTrace);
    }
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
