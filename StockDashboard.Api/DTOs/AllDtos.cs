namespace StockDashboard.Api.DTOs;

public record CompanyDto(int Id, string Symbol, string Name);

public record StockPriceDto(
    DateTime Date,
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    long Volume,
    decimal? DailyReturn,
    decimal? MovingAverage7,
    decimal? Volatility14
);

public record StockSummaryDto(
    string Symbol,
    decimal FiftyTwoWeekHigh,
    decimal FiftyTwoWeekLow,
    decimal AverageClose
);

public record CompareDto(
    string Symbol1,
    string Symbol2,
    IReadOnlyList<StockPriceDto> Data1,
    IReadOnlyList<StockPriceDto> Data2
);
