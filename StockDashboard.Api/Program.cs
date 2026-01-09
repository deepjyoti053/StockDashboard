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
// Enable Swagger in all environments for demo purposes
app.UseSwagger();
app.UseSwaggerUI();

// Auto-migrate and Seed on startup
using (var scope = app.Services.CreateScope())
{
    // Custom File Logger for debugging
    void Log(string message) 
    {
        try {
            Console.WriteLine(message);
            File.AppendAllText("startup_log.txt", $"{DateTime.UtcNow}: {message}\n");
        } catch {}
    }

    try 
    {
        Log("[Startup] Starting application...");
        var scopeProvider = scope.ServiceProvider;
        var db = scopeProvider.GetRequiredService<AppDbContext>();
        
        Log("[Startup] Applying Database Migrations...");
        db.Database.Migrate();
        Log("[Startup] Database Migrations Applied Successfully.");

        var ingestion = scopeProvider.GetRequiredService<StockIngestionService>();
        
        // Debugging Paths
        var cwd = Directory.GetCurrentDirectory();
        Log($"[Seeding] Current Working Directory: {cwd}");
        
        var infyPath = Path.Combine(cwd, "Data", "INFY.csv");
        var tcsPath = Path.Combine(cwd, "Data", "TCS.csv");

        Log($"[Seeding] Looking for INFY at: {infyPath}");
        if (File.Exists(infyPath)) 
        {
            Log("[Seeding] INFY.csv found. Importing...");
            await ingestion.ImportFromCsvAsync("INFY", "Infosys Ltd", infyPath);
        }
        else
        {
            Log("[Seeding] ERROR: INFY.csv NOT FOUND.");
        }

        if (File.Exists(tcsPath))
        {
             Log("[Seeding] TCS.csv found. Importing...");
            await ingestion.ImportFromCsvAsync("TCS", "Tata Consultancy Services", tcsPath);
        }
        else
        {
            Log("[Seeding] ERROR: TCS.csv NOT FOUND.");
        }
        Log("[Seeding] Framework initialization complete.");

    } catch (Exception ex) {
        Log($"[Startup] FATAL ERROR during Migration/Seeding: {ex.Message}");
        Log(ex.StackTrace);
    }
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
