using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace E_commerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action Methods
        // An action result is one of the custom classes or rather interface
        // that is implemented in the..Net framework and that basically
        // implements all of the possible result type for an action method.
        public IActionResult Index()
        {
            // if leave it empty, it will use Action method name
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
    }
}
