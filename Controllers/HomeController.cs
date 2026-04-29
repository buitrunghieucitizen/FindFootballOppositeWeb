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
            return View("Guest/Index", _portalData.BuildPortal());
        }

        public IActionResult Teams()
        {
            return View("Guest/Teams", _portalData.BuildPortal());
        }

        public IActionResult Stadiums()
        {
            return View("Guest/Stadiums", _portalData.BuildPortal());
        }

        public IActionResult Matches()
        {
            return View("Guest/Matches", _portalData.BuildPortal());
        }

        public IActionResult Recruitment()
        {
            return View("Guest/Recruitment", _portalData.BuildPortal());
        }

        public IActionResult Operations()
        {
            return View("Guest/Operations", _portalData.BuildPortal());
        }

        public IActionResult Admin()
        {
            return View("Admin/Admin", _portalData.BuildPortal());
        }

        public IActionResult StadiumOwner()
        {
            return View("StadiumOwner/StadiumOwner", _portalData.BuildPortal());
        }

        public IActionResult Captain()
        {
            return View("Captain/Captain", _portalData.BuildPortal());
        }

        public IActionResult Privacy()
        {
            return View("Guest/Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
