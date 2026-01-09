using CsvHelper;
using CsvHelper.Configuration;
using StockDashboard.Api.Data;
using StockDashboard.Api.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace StockDashboard.Api.Services;

public class StockIngestionService
{
    private readonly AppDbContext _db;

    public StockIngestionService(AppDbContext db)
    {
        _db = db;
    }

    public async Task ImportFromCsvAsync(string symbol, string companyName, string csvPath)
    {
        var company = await _db.Companies
            .SingleOrDefaultAsync(c => c.Symbol == symbol);

        if (company == null)
        {
            company = new Company { Symbol = symbol, Name = companyName };
            _db.Companies.Add(company);
            await _db.SaveChangesAsync();
        }

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null,
        };

        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, config);

        var records = new List<StockPrice>();

        // Read Header
        await csv.ReadAsync();
        csv.ReadHeader();

        // Flexible reading if CSV headers differ slightly, but assuming Date,Open,High,Low,Close,Volume
        while (await csv.ReadAsync())
        {
             // Check if already exists to avoid duplicates if running multiple times (though slow for bulk)
             // Ideally we clear or upsert. For this assignment, we skip or assume clean db.
             // We'll read all into memory first.
             
            try 
            {
                var date = csv.GetField<DateTime>("Date");
                var open = csv.GetField<decimal>("Open");
                var high = csv.GetField<decimal>("High");
                var low = csv.GetField<decimal>("Low");
                var close = csv.GetField<decimal>("Close");
                var volume = csv.GetField<long>("Volume");

                records.Add(new StockPrice
                {
                    CompanyId = company.Id,
                    Date = date,
                    Open = open,
                    High = high,
                    Low = low,
                    Close = close,
                    Volume = volume
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing row: {ex.Message}");
                // specific row error, skip
            }
        }
        
        Console.WriteLine($"Parsed {records.Count} records for {symbol}");


        // Sort by date ASC
        records = records.OrderBy(r => r.Date).ToList();

        // Calculate metrics
        CalculateMetrics(records);

        // Batch insert - check for existing to be safe
        var existingDates = await _db.StockPrices
            .Where(p => p.CompanyId == company.Id)
            .Select(p => p.Date)
            .ToListAsync();
        
        var existingSet = new HashSet<DateTime>(existingDates);
        
        var newRecords = records.Where(r => !existingSet.Contains(r.Date)).ToList();

        if (newRecords.Any())
        {
            _db.StockPrices.AddRange(newRecords);
            await _db.SaveChangesAsync();
        }
    }

    private void CalculateMetrics(List<StockPrice> prices)
    {
        const int maWindow = 7;
        const int volWindow = 14;

        for (int i = 0; i < prices.Count; i++)
        {
            var p = prices[i];

            // Daily Return = (CLOSE - OPEN) / OPEN
            if (p.Open != 0)
            {
                p.DailyReturn = (p.Close - p.Open) / p.Open;
            }

            // 7-day Moving Average on Close
            if (i >= maWindow - 1)
            {
                var sum = 0m;
                for (int j = 0; j < maWindow; j++)
                {
                    sum += prices[i - j].Close;
                }
                p.MovingAverage7 = sum / maWindow;
            }

            // Volatility (custom metric) – std dev of daily returns over last 14 days
            if (i >= volWindow - 1)
            {
                var windowValues = new List<decimal>();
                for(int j=0; j<volWindow; j++)
                {
                    var val = prices[i-j].DailyReturn;
                    if(val.HasValue) windowValues.Add(val.Value);
                }

                if (windowValues.Count == volWindow)
                {
                    var avg = windowValues.Average();
                    var sumSqDiff = windowValues.Sum(r => (r - avg) * (r - avg));
                    var variance = sumSqDiff / volWindow;
                    p.Volatility14 = (decimal)Math.Sqrt((double)variance);
                }
            }
        }
    }
}
