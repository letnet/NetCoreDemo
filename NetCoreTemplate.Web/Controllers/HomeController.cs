using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreTemplate.Application;
using NetCoreTemplate.Web.Models;
using NLog;

namespace NetCoreTemplate.Web.Controllers
{
    public class HomeController : Controller
    {
        ITestService _testService { get; set; }
        public HomeController(ITestService testService)
        {
            this._testService = testService;
        }

        public IActionResult Index()
        {
           var testDto =  _testService.Get("68f4469d-1222-4414-bf73-6823815cdb15");
            return View(testDto);
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
