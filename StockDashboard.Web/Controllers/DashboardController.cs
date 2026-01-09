using Microsoft.AspNetCore.Mvc;

namespace StockDashboard.Web.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
