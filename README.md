# 🚀 Stock Dashboard - Financial Data Intelligence Platform

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4)](https://docs.microsoft.com/aspnet/core)
[![Entity Framework](https://img.shields.io/badge/EF%20Core-8.0-512BD4)](https://docs.microsoft.com/ef/core)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-CC2927?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Chart.js](https://img.shields.io/badge/Chart.js-4.x-FF6384?logo=chartdotjs)](https://www.chartjs.org/)

A modern, full-stack financial data platform built with **ASP.NET Core** that ingests, processes, and visualizes stock market data with real-time analytics and interactive dashboards.

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [Technology Stack](#-technology-stack)
- [Prerequisites](#-prerequisites)
- [Installation & Setup](#-installation--setup)
- [Running the Application](#-running-the-application)
- [API Documentation](#-api-documentation)
- [Data Model](#-data-model)
- [Custom Metrics](#-custom-metrics)
- [Project Structure](#-project-structure)
- [Screenshots](#-screenshots)
- [Contributing](#-contributing)
- [License](#-license)

## 🎯 Overview

**Stock Dashboard** is a mini financial data intelligence platform that demonstrates comprehensive skills in:

- 📊 **Data Collection & Processing** - Ingests CSV stock data with automatic cleaning and validation
- 🔄 **REST API Development** - Exposes well-designed endpoints following RESTful principles
- 📈 **Data Analytics** - Calculates financial metrics including daily returns, moving averages, and volatility
- 🎨 **Interactive Visualization** - Real-time charts with Chart.js showing price trends and analytics
- 🏗️ **Clean Architecture** - Separation of concerns with layered design pattern

This project was built as part of an internship assignment to showcase technical proficiency in building production-grade financial applications.

## ✨ Features

### Core Features

- ✅ **Automated Data Ingestion** - Imports stock data from CSV files with automatic validation
- ✅ **Financial Metrics Calculation**
  - Daily Return: `(Close - Open) / Open`
  - 7-Day Moving Average on closing prices
  - 14-Day Volatility (Standard deviation of daily returns)
- ✅ **RESTful API** - Well-documented endpoints with Swagger/OpenAPI support
- ✅ **Interactive Dashboard** - SPA-style web interface with real-time updates
- ✅ **52-Week Statistics** - High, low, and average close price tracking
- ✅ **Stock Comparison** - Compare performance of two stocks side-by-side

### Advanced Features

- 🔍 **Swagger API Documentation** - Interactive API testing interface
- 🎯 **Entity Framework Migrations** - Automated database schema management
- 🚀 **Auto-Seeding** - Database automatically populates on first run
- 📱 **Responsive UI** - Bootstrap 5-based responsive design
- ⚡ **CORS Enabled** - Cross-origin resource sharing for frontend integration

## 🏛️ Architecture

The application follows a **clean layered architecture** pattern:

```
┌─────────────────────────────────────────────────────┐
│                   Presentation Layer                │
│  (MVC Views, API Controllers, JavaScript/Chart.js)  │
└─────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────┐
│                   Service Layer                     │
│    (StockIngestionService, StockQueryService)       │
└─────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────┐
│                   Data Access Layer                 │
│         (Entity Framework Core, AppDbContext)       │
└─────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────┐
│                   Database Layer                    │
│                  (SQL Server LocalDB)               │
└─────────────────────────────────────────────────────┘
```

### Project Structure

```
StockDashboard/
├── StockDashboard.Api/          # Backend Web API
│   ├── Controllers/             # API endpoints
│   ├── Services/                # Business logic layer
│   ├── Models/                  # Domain entities
│   ├── DTOs/                    # Data transfer objects
│   ├── Data/                    # EF Core DbContext & Migrations
│   │   ├── AppDbContext.cs
│   │   ├── INFY.csv            # Sample data (Infosys)
│   │   └── TCS.csv             # Sample data (TCS)
│   └── Program.cs              # Application entry point
│
├── StockDashboard.Web/          # Frontend MVC Application
│   ├── Controllers/             # MVC controllers
│   ├── Views/                   # Razor views
│   │   ├── Dashboard/
│   │   │   └── Index.cshtml    # Main dashboard view
│   │   └── Shared/
│   │       └── _Layout.cshtml  # Layout template
│   └── wwwroot/                # Static files
│       ├── css/
│       └── js/
│           └── dashboard.js    # Dashboard JavaScript logic
│
└── README.md                    # This file
```

## 🛠️ Technology Stack

### Backend
- **Framework**: ASP.NET Core 8.0 Web API
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server (LocalDB for development)
- **API Documentation**: Swashbuckle.AspNetCore (Swagger)
- **CSV Parsing**: CsvHelper 33.1.0

### Frontend
- **Framework**: ASP.NET Core 8.0 MVC
- **UI Library**: Bootstrap 5
- **Charts**: Chart.js 4.x
- **JavaScript**: Vanilla ES6+

### Development Tools
- **.NET SDK**: 8.0
- **IDE**: Visual Studio 2022 / Visual Studio Code / Rider
- **Database Tools**: SQL Server Management Studio / Azure Data Studio (optional)

## 📦 Prerequisites

Before you begin, ensure you have the following installed:

- ✅ [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- ✅ [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or [VS Code](https://code.visualstudio.com/)
- ✅ [SQL Server LocalDB](https://docs.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb) (included with Visual Studio)
- ✅ [Git](https://git-scm.com/) (for version control)

### Verify Installation

```bash
# Check .NET version
dotnet --version
# Should output: 8.0.x or higher

# Check SQL Server LocalDB
sqllocaldb info
# Should list available instances
```

## 🚀 Installation & Setup

### Option 1: Using Command Line

1. **Clone the repository**
```bash
git clone <your-repo-url>
cd StockDashboard
```

2. **Restore NuGet packages**
```bash
dotnet restore
```

3. **Update database connection string** (optional)

Edit `StockDashboard.Api/appsettings.json` if you want to use a different SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost;Initial Catalog=StockDashboard;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=\"StockDashboard\""
  }
}
```

4. **Apply database migrations**

The database will be created automatically on first run with auto-migration. Alternatively, you can run manually:

```bash
cd StockDashboard.Api
dotnet ef database update
```

### Option 2: Using Visual Studio

1. **Open Solution**
   - Open `StockDashboard.sln` in Visual Studio 2022

2. **Restore Packages**
   - Right-click on the solution → `Restore NuGet Packages`

3. **Set Startup Projects**
   - Right-click on Solution → `Properties`
   - Select `Multiple startup projects`
   - Set both `StockDashboard.Api` and `StockDashboard.Web` to **Start**

4. **Build Solution**
   - Press `Ctrl+Shift+B` or use `Build → Build Solution`

## 🎮 Running the Application

### Method 1: Command Line

You need to run both projects simultaneously.

**Terminal 1 - API Server:**
```bash
cd StockDashboard.Api
dotnet run
```
Expected output:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7249
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5104
```

**Terminal 2 - Web Frontend:**
```bash
cd StockDashboard.Web
dotnet run
```
Expected output:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7204
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5126
```

### Method 2: Visual Studio

1. Press `F5` or click **Start** (both projects will launch)
2. Two browser windows will open automatically

### Access Points

Once running, you can access:

| Service | URL | Description |
|---------|-----|-------------|
| **Dashboard UI** | https://localhost:7204/Dashboard | Interactive stock visualization |
| **API Swagger** | https://localhost:7249/swagger | API documentation & testing |
| **API Base** | https://localhost:7249/api | REST API endpoints |

> **Note**: Port numbers may vary. Check your console output for exact URLs.

### First Run Behavior

On the first run, the application will:

1. ✅ Create the SQL Server database `StockDashboard`
2. ✅ Run EF Core migrations to create tables
3. ✅ Automatically import sample data from CSV files (`INFY.csv`, `TCS.csv`)
4. ✅ Calculate all financial metrics (daily return, moving averages, volatility)

You should see console output like:
```
[Seeding] Current Working Directory: D:\Projects\StockDashboard\StockDashboard.Api
[Seeding] Looking for INFY at: D:\Projects\StockDashboard\StockDashboard.Api\Data\INFY.csv
[Seeding] INFY.csv found. Importing...
Parsed 30 records for INFY
[Seeding] TCS.csv found. Importing...
Parsed 30 records for TCS
```

## 📚 API Documentation

### Base URL
```
https://localhost:7249/api
```

### Endpoints

#### 1. Get All Companies
```http
GET /api/companies
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "symbol": "INFY",
    "name": "Infosys Ltd"
  },
  {
    "id": 2,
    "symbol": "TCS",
    "name": "Tata Consultancy Services"
  }
]
```

---

#### 2. Get Last 30 Days Stock Data
```http
GET /api/data/{symbol}
```

**Parameters:**
- `symbol` (path) - Stock symbol (e.g., "INFY", "TCS")

**Response:** `200 OK`
```json
[
  {
    "date": "2024-12-01T00:00:00",
    "open": 1500.00,
    "high": 1520.00,
    "low": 1495.00,
    "close": 1510.00,
    "volume": 1200000,
    "dailyReturn": 0.0067,
    "movingAverage7": 1505.50,
    "volatility14": 0.0123
  }
]
```

**Error Response:** `404 Not Found`
```json
"No data found for symbol XYZ"
```

---

#### 3. Get 52-Week Summary
```http
GET /api/summary/{symbol}
```

**Parameters:**
- `symbol` (path) - Stock symbol

**Response:** `200 OK`
```json
{
  "symbol": "INFY",
  "fiftyTwoWeekHigh": 1650.00,
  "fiftyTwoWeekLow": 1200.00,
  "averageClose": 1425.75
}
```

---

#### 4. Compare Two Stocks (Bonus)
```http
GET /api/compare?symbol1=INFY&symbol2=TCS
```

**Query Parameters:**
- `symbol1` - First stock symbol
- `symbol2` - Second stock symbol

**Response:** `200 OK`
```json
{
  "symbol1": "INFY",
  "symbol2": "TCS",
  "data1": [ /* Last 90 days data for INFY */ ],
  "data2": [ /* Last 90 days data for TCS */ ]
}
```

### Testing with Swagger

1. Navigate to https://localhost:7249/swagger
2. Expand any endpoint
3. Click **"Try it out"**
4. Enter parameters and click **"Execute"**

### Testing with cURL

```bash
# Get companies
curl -X GET "https://localhost:7249/api/companies" -k

# Get stock data
curl -X GET "https://localhost:7249/api/data/INFY" -k

# Get summary
curl -X GET "https://localhost:7249/api/summary/TCS" -k

# Compare stocks
curl -X GET "https://localhost:7249/api/compare?symbol1=INFY&symbol2=TCS" -k
```

> **Note**: `-k` flag bypasses SSL certificate validation for development

## 🗄️ Data Model

### Entity Relationship Diagram

```
┌─────────────────┐          ┌──────────────────────┐
│    Company      │          │     StockPrice       │
├─────────────────┤          ├──────────────────────┤
│ Id (PK)         │──┐    ┌──│ Id (PK)              │
│ Symbol          │  │    │  │ CompanyId (FK)       │
│ Name            │  └────┘  │ Date                 │
└─────────────────┘          │ Open                 │
                             │ High                 │
                             │ Low                  │
                             │ Close                │
                             │ Volume               │
                             │ DailyReturn          │
                             │ MovingAverage7       │
                             │ Volatility14         │
                             └──────────────────────┘
```

### Database Schema

**Companies Table:**
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key (auto-increment) |
| Symbol | nvarchar(10) | Unique stock symbol |
| Name | nvarchar(200) | Company name |

**StockPrices Table:**
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key (auto-increment) |
| CompanyId | int | Foreign key to Companies |
| Date | datetime2 | Trading date |
| Open | decimal(18,2) | Opening price |
| High | decimal(18,2) | Highest price |
| Low | decimal(18,2) | Lowest price |
| Close | decimal(18,2) | Closing price |
| Volume | bigint | Trading volume |
| DailyReturn | decimal(18,4) | Calculated metric |
| MovingAverage7 | decimal(18,2) | Calculated metric |
| Volatility14 | decimal(18,4) | Calculated metric |

**Indexes:**
- Unique index on `Company.Symbol`
- Unique composite index on `StockPrice(CompanyId, Date)`

## 📊 Custom Metrics

### 1. Daily Return
Measures the percentage change from open to close price.

```
Daily Return = (Close - Open) / Open
```

**Example:** 
- Open: ₹1500
- Close: ₹1520
- Daily Return: (1520 - 1500) / 1500 = **0.0133** (1.33%)

**Use Case:** Identifies daily performance and trading patterns

---

### 2. 7-Day Moving Average
Smooths out price fluctuations by averaging the last 7 closing prices.

```
MA(7) = (Close₁ + Close₂ + ... + Close₇) / 7
```

**Use Case:** 
- Identifies short-term price trends
- Buy signal when price crosses above MA
- Sell signal when price crosses below MA

---

### 3. 14-Day Volatility (Custom Metric)
Measures price stability using standard deviation of daily returns.

```
σ = √(Σ(Return - μ)² / N)
where μ = average return, N = 14 days
```

**Interpretation:**
- **Low Volatility (< 0.01)**: Stable stock, lower risk
- **Medium Volatility (0.01-0.03)**: Moderate fluctuation
- **High Volatility (> 0.03)**: Unstable, higher risk/reward

**Use Case:** 
- Risk assessment
- Portfolio diversification
- Options pricing inputs

---

### 4. 52-Week High/Low
Tracks the extreme prices over the past year.

**Use Cases:**
- Identify support/resistance levels
- Evaluate current price relative to historical range
- Detect breakouts or breakdowns

## 📁 Project Structure

```
StockDashboard/
│
├── StockDashboard.sln                     # Visual Studio solution file
├── README.md                              # This file
│
├── StockDashboard.Api/                    # Backend API Project
│   ├── StockDashboard.Api.csproj         # Project file
│   ├── Program.cs                         # App entry point & configuration
│   ├── appsettings.json                   # Configuration (DB connection, etc.)
│   │
│   ├── Controllers/
│   │   ├── CompaniesController.cs        # GET /api/companies
│   │   └── StockDataController.cs        # GET /api/data, /api/summary, /api/compare
│   │
│   ├── Services/
│   │   ├── StockIngestionService.cs      # CSV import & metric calculation
│   │   └── StockQueryService.cs          # Business logic for API queries
│   │
│   ├── Models/
│   │   ├── Company.cs                    # Company entity
│   │   └── StockPrice.cs                 # StockPrice entity
│   │
│   ├── DTOs/
│   │   ├── CompanyDto.cs
│   │   ├── StockPriceDto.cs
│   │   ├── StockSummaryDto.cs
│   │   └── CompareDto.cs
│   │
│   ├── Data/
│   │   ├── AppDbContext.cs               # EF Core DbContext
│   │   ├── INFY.csv                      # Sample data - Infosys
│   │   ├── TCS.csv                       # Sample data - TCS
│   │   └── Migrations/                   # EF Core migrations (auto-generated)
│   │
│   └── Properties/
│       └── launchSettings.json           # Development server configuration
│
└── StockDashboard.Web/                    # Frontend MVC Project
    ├── StockDashboard.Web.csproj         # Project file
    ├── Program.cs                        # App entry point
    ├── appsettings.json
    │
    ├── Controllers/
    │   ├── DashboardController.cs        # Dashboard page controller
    │   └── HomeController.cs             # Home page controller
    │
    ├── Views/
    │   ├── Dashboard/
    │   │   └── Index.cshtml              # Main dashboard view
    │   ├── Home/
    │   │   ├── Index.cshtml              # Landing page
    │   │   └── Privacy.cshtml
    │   ├── Shared/
    │   │   ├── _Layout.cshtml            # Main layout template
    │   │   ├── Error.cshtml
    │   │   └── _ValidationScriptsPartial.cshtml
    │   ├── _ViewImports.cshtml
    │   └── _ViewStart.cshtml
    │
    └── wwwroot/                          # Static files (served directly)
        ├── css/
        │   └── site.css                  # Custom styles
        ├── js/
        │   └── dashboard.js              # Dashboard logic & Chart.js integration
        ├── lib/                          # Third-party libraries (Bootstrap, jQuery)
        └── favicon.ico
```

## 🖼️ Screenshots

### Dashboard Interface
The main dashboard features:
- 📋 **Left Panel**: Company list with click-to-select
- 📊 **Statistics Cards**: 52-week high, low, and average close prices
- 📈 **Interactive Chart**: Closing prices + 7-day moving average

### Swagger API Documentation
Interactive API testing interface with:
- 📝 Endpoint documentation
- 🧪 Try-it-out functionality
- 📋 Request/response examples

## 🔧 Configuration

### Database Configuration

Edit `StockDashboard.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost;Initial Catalog=StockDashboard;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

**For PostgreSQL:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=StockDashboard;Username=postgres;Password=yourpassword"
  }
}
```
Also update `Program.cs`:
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

### API Base URL (Frontend)

Edit `StockDashboard.Web/wwwroot/js/dashboard.js`:

```javascript
const API_BASE_URL = 'https://localhost:7249/api';  // Update port if needed
```

### Adding More Stock Data

1. Create a CSV file in `StockDashboard.Api/Data/` with this format:
```csv
Date,Open,High,Low,Close,Volume
2024-12-01,1500,1520,1495,1510,1200000
2024-12-02,1510,1530,1500,1525,1100000
```

2. Edit `StockDashboard.Api/Program.cs` to import the new file:
```csharp
var reliancePath = Path.Combine(cwd, "Data", "RELIANCE.csv");
if (File.Exists(reliancePath))
{
    await ingestion.ImportFromCsvAsync("RELIANCE", "Reliance Industries", reliancePath);
}
```

## 🧪 Testing

### Manual Testing Checklist

- [ ] API returns companies list
- [ ] Stock data displays for selected company
- [ ] Chart renders correctly with moving average
- [ ] Summary statistics show correct values
- [ ] Compare API returns data for both stocks
- [ ] Database persists data across restarts

### API Testing with Postman

Import this collection:

```json
{
  "info": { "name": "Stock Dashboard API" },
  "item": [
    {
      "name": "Get Companies",
      "request": {
        "method": "GET",
        "url": "https://localhost:7249/api/companies"
      }
    }
  ]
}
```

## 🐛 Troubleshooting

### Issue: Database Connection Error

**Error:** `Cannot open database "StockDashboard" requested by the login.`

**Solution:**
```bash
cd StockDashboard.Api
dotnet ef database drop    # Warning: deletes all data
dotnet ef database update
```

Or check SQL Server is running:
```bash
sqllocaldb start MSSQLLocalDB
```

---

### Issue: CSV Files Not Found

**Error:** `[Seeding] ERROR: INFY.csv NOT FOUND.`

**Solution:** 
Ensure CSV files are in `StockDashboard.Api/Data/` and are set to **Copy to Output Directory**.

Edit `.csproj`:
```xml
<ItemGroup>
  <None Update="Data\*.csv">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

---

### Issue: CORS Error in Browser Console

**Error:** `Access to fetch at 'https://localhost:7249/api/companies' has been blocked by CORS policy`

**Solution:** Already handled in `Program.cs` with `AllowAll` policy. If issue persists:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeb",
        b => b.WithOrigins("https://localhost:7204")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

app.UseCors("AllowWeb");
```

---

### Issue: Port Already in Use

**Error:** `Failed to bind to address https://127.0.0.1:7249: address already in use.`

**Solution:** Change port in `Properties/launchSettings.json`:

```json
"applicationUrl": "https://localhost:7250;http://localhost:5105"
```

---

### Issue: Swagger Not Loading

**Solution:** Ensure you're in Development mode:

```bash
# Windows PowerShell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run

# Linux/Mac
export ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

Or add to `launchSettings.json`:
```json
"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development"
}
```

## 🚀 Deployment

### Azure App Service

1. **Publish API:**
```bash
cd StockDashboard.Api
dotnet publish -c Release -o ./publish
```

2. **Create Azure Resources:**
```bash
az webapp create --resource-group MyResourceGroup \
  --plan MyAppServicePlan --name StockDashboardApi \
  --runtime "DOTNETCORE|8.0"
```

3. **Deploy:**
```bash
az webapp deployment source config-zip \
  --resource-group MyResourceGroup \
  --name StockDashboardApi \
  --src ./publish.zip
```

4. **Update Connection String** in Azure Portal → Configuration → Application Settings

### Docker Deployment

Create `Dockerfile` in `StockDashboard.Api/`:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["StockDashboard.Api/StockDashboard.Api.csproj", "StockDashboard.Api/"]
RUN dotnet restore "StockDashboard.Api/StockDashboard.Api.csproj"
COPY . .
WORKDIR "/src/StockDashboard.Api"
RUN dotnet build "StockDashboard.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockDashboard.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockDashboard.Api.dll"]
```

Build and run:
```bash
docker build -t stockdashboard-api .
docker run -p 8080:80 stockdashboard-api
```

## 📝 Development Notes

### Key Design Decisions

1. **SQL Server over SQLite** - Better performance for financial data queries
2. **Stored Metrics** - Pre-calculated vs. on-the-fly for better API response times
3. **Auto-Migration** - Simplifies first-run experience for reviewers
4. **CORS AllowAll** - Development convenience (should be restricted in production)

### Future Enhancements

- [ ] Add authentication/authorization (JWT tokens)
- [ ] Implement caching (Redis) for frequently accessed data
- [ ] Add real-time data ingestion via scheduled jobs (Hangfire)
- [ ] Integrate machine learning for price prediction
- [ ] Add WebSocket support for live price updates
- [ ] Implement pagination for large datasets
- [ ] Add unit tests (xUnit) and integration tests
- [ ] Dockerize the entire application
- [ ] Add CI/CD pipeline (GitHub Actions)

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Code Style
- Follow [C# Coding Conventions](https://docs.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use PascalCase for public members
- Use camelCase for private fields (prefixed with `_`)
- Add XML documentation comments for public APIs

## 📄 License

This project is licensed under the MIT License - see below for details:

```
MIT License

Copyright (c) 2026 [Your Name]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## 👨‍💻 Author

- GitHub: [deepjyoti053](https://github.com/deepjyoti053)
- LinkedIn: [deep-jyoti-971512176](https://www.linkedin.com/in/deep-jyoti-971512176)
- Email: deepjyoti053@gmail.com

## 🙏 Acknowledgments

- Assignment provided by **Jarnox** for internship selection
- Stock data format inspired by NSE/BSE bhavcopy CSVs
- Chart.js for beautiful visualizations
- ASP.NET Core team for excellent documentation

---

## 📚 Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Chart.js Documentation](https://www.chartjs.org/docs)
- [Bootstrap 5](https://getbootstrap.com/docs/5.0)
- [Swagger/OpenAPI](https://swagger.io/specification/)

---

<div align="center">

**Built with ❤️ using ASP.NET Core**

⭐ Star this repository if you found it helpful!

</div>
