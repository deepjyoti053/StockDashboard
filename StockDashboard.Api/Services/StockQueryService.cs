using Microsoft.EntityFrameworkCore;
using StockDashboard.Api.Data;
using StockDashboard.Api.DTOs;
using StockDashboard.Api.Models;

namespace StockDashboard.Api.Services;

public class StockQueryService
{
    private readonly AppDbContext _db;

    public StockQueryService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<CompanyDto>> GetCompaniesAsync()
    {
        return await _db.Companies
            .OrderBy(c => c.Symbol)
            .Select(c => new CompanyDto(c.Id, c.Symbol, c.Name))
            .ToListAsync();
    }

    public async Task<List<StockPriceDto>> GetLast30DaysAsync(string symbol)
    {
        var company = await _db.Companies
            .SingleOrDefaultAsync(c => c.Symbol == symbol);

        if (company == null) return new List<StockPriceDto>();

        var maxDate = await _db.StockPrices
            .Where(p => p.CompanyId == company.Id)
            .MaxAsync(p => (DateTime?)p.Date);

        if (maxDate == null) return new List<StockPriceDto>();

        var fromDate = maxDate.Value.AddDays(-30);

        return await _db.StockPrices
            .Where(p => p.CompanyId == company.Id && p.Date >= fromDate)
            .OrderBy(p => p.Date)
            .Select(p => new StockPriceDto(
                p.Date, p.Open, p.High, p.Low, p.Close, p.Volume,
                p.DailyReturn, p.MovingAverage7, p.Volatility14
            ))
            .ToListAsync();
    }

    public async Task<StockSummaryDto?> GetSummaryAsync(string symbol)
    {
        var company = await _db.Companies
            .SingleOrDefaultAsync(c => c.Symbol == symbol);

        if (company == null) return null;

        var maxDate = await _db.StockPrices
            .Where(p => p.CompanyId == company.Id)
            .MaxAsync(p => (DateTime?)p.Date);

        if (maxDate == null) return null;

        // Summary typically based on 52 weeks (1 year)
        var fromDate = maxDate.Value.AddDays(-365);

        var query = _db.StockPrices
            .Where(p => p.CompanyId == company.Id && p.Date >= fromDate);

        // We need to execute aggregations. 
        // Note: If no data in range, Max/Min might throw or return null depending on type.
        // But we checked maxDate exists.
        
        var high = await query.MaxAsync(p => p.High); 
        // Requirements say "52-week High/Low". Usually High/Low columns, or High/Low of Close? 
        // Standard is High of Highs, Low of Lows.
        var low = await query.MinAsync(p => p.Low);
        var avg = await query.AverageAsync(p => p.Close);

        return new StockSummaryDto(symbol, high, low, avg);
    }

    public async Task<CompareDto?> CompareAsync(string symbol1, string symbol2)
    {
        var c1 = await _db.Companies.SingleOrDefaultAsync(c => c.Symbol == symbol1);
        var c2 = await _db.Companies.SingleOrDefaultAsync(c => c.Symbol == symbol2);

        if (c1 == null || c2 == null) return null;

        // Find common max date or just max date of each?
        // Usually comparision is last X days. Let's say last 90 days.
        var maxDate1 = await _db.StockPrices.Where(p => p.CompanyId == c1.Id).MaxAsync(p => (DateTime?)p.Date);
        var maxDate2 = await _db.StockPrices.Where(p => p.CompanyId == c2.Id).MaxAsync(p => (DateTime?)p.Date);
        
        if (maxDate1 == null || maxDate2 == null) return null;
        
        // Take the earlier of the two max dates or just today? 
        // Let's take the latest available date broadly.
        var maxDate = maxDate1 > maxDate2 ? maxDate1 : maxDate2;
        var fromDate = maxDate!.Value.AddDays(-90);

        var data1 = await _db.StockPrices
            .Where(p => p.CompanyId == c1.Id && p.Date >= fromDate)
            .OrderBy(p => p.Date)
            .Select(p => new StockPriceDto(
                p.Date, p.Open, p.High, p.Low, p.Close, p.Volume,
                p.DailyReturn, p.MovingAverage7, p.Volatility14
            ))
            .ToListAsync();

        var data2 = await _db.StockPrices
            .Where(p => p.CompanyId == c2.Id && p.Date >= fromDate)
            .OrderBy(p => p.Date)
            .Select(p => new StockPriceDto(
                p.Date, p.Open, p.High, p.Low, p.Close, p.Volume,
                p.DailyReturn, p.MovingAverage7, p.Volatility14
            ))
            .ToListAsync();

        return new CompareDto(symbol1, symbol2, data1, data2);
    }
}
