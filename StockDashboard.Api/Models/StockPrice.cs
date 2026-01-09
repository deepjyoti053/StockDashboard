using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockDashboard.Api.Models;

public class StockPrice
{
    public int Id { get; set; }
    
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public DateTime Date { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Open { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal High { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Low { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Close { get; set; }
    
    public long Volume { get; set; }

    // Calculated metrics
    [Column(TypeName = "decimal(18,4)")]
    public decimal? DailyReturn { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? MovingAverage7 { get; set; }
    
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Volatility14 { get; set; }
}
