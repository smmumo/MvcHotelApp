using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcHotelApp.Models;

namespace MvcHotelApp.Controllers
{
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

        /*
        //useful  learning links
        https://www.tutorialsteacher.com/linq/linq-tutorials
        https://www.dotnettricks.com/learn/linq

        https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/collections

        https://www.tutorialsteacher.com/csharp/csharp-collection
        
        */
    }
}
