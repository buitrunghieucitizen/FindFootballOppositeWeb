using System.Diagnostics;
using FindFootballOppsite.Models;
using FindFootballOppsite.Services;
using Microsoft.AspNetCore.Mvc;

namespace FindFootballOppsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PortalDataService _portalData;

        public HomeController(ILogger<HomeController> logger, PortalDataService portalData)
        {
            _logger = logger;
            _portalData = portalData;
        }

        public IActionResult Index()
        {
            return View(_portalData.BuildPortal());
        }

        public IActionResult Teams()
        {
            return View(_portalData.BuildPortal());
        }

        public IActionResult Stadiums()
        {
            return View(_portalData.BuildPortal());
        }

        public IActionResult Matches()
        {
            return View(_portalData.BuildPortal());
        }

        public IActionResult Recruitment()
        {
            return View(_portalData.BuildPortal());
        }

        public IActionResult Operations()
        {
            return View(_portalData.BuildPortal());
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
