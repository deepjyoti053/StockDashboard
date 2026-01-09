using Microsoft.EntityFrameworkCore;
using StockDashboard.Api.Models;

namespace StockDashboard.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<StockPrice> StockPrices => Set<StockPrice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>()
            .HasIndex(c => c.Symbol)
            .IsUnique();

        modelBuilder.Entity<StockPrice>()
            .HasIndex(sp => new { sp.CompanyId, sp.Date })
            .IsUnique();
    }
}
