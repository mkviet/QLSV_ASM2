using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSVVV.Models;
using System.Data;
using System.Diagnostics;

namespace QLSVVV.Controllers
{
    /*[Authorize(Roles = "Admin")]
    [Authorize(Roles = "Student")]
    [Authorize(Roles = "Teacher")]*/
 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult Login()
        {
            return View();
        }
    }
}