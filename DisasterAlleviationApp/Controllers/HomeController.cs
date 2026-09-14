using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Services; // Import our new service namespace
using System.Threading.Tasks;

namespace DisasterAlleviationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DisasterApiService _disasterApiService; // Field for our API service

        // Injecting the service via the controller constructor
        public HomeController(ILogger<HomeController> logger, DisasterApiService disasterApiService)
        {
            _logger = logger;
            _disasterApiService = disasterApiService;
        }

        // Asynchronous Index action method that fetches live disaster feeds
        public async Task<IActionResult> Index()
        {
            // Call our service to fetch active events from the public API
            var disasters = await _disasterApiService.GetActiveDisastersAsync();

            // Pass the retrieved list of disasters directly into the Razor view
            return View(disasters);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}