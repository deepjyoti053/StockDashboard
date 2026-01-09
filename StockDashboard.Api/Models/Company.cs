using System.ComponentModel.DataAnnotations;

namespace StockDashboard.Api.Models;

public class Company
{
    public int Id { get; set; }
    
    [Required]
    public string Symbol { get; set; } = null!;
    
    [Required]
    public string Name { get; set; } = null!;

    public ICollection<StockPrice> Prices { get; set; } = new List<StockPrice>();
}
