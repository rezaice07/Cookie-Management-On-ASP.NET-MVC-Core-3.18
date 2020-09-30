using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCore.Cookie.Models;
using AspNetCore.Cookie.TrackingServices;

namespace AspNetCore.Cookie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAddToCartEventTraking _addToCartEventTraking;

        public HomeController(ILogger<HomeController> logger,
            IAddToCartEventTraking addToCartEventTraking
            )
        {
            _logger = logger;
            this._addToCartEventTraking = addToCartEventTraking;
        }

        public IActionResult Index()
        {
            var patientLabTestKey = "QWR45-464654-HGE9879_jjlj45";

            //track or set value into the cookie
            _addToCartEventTraking.TrackToAddCartIdentifier(patientLabTestKey);

            return View();
        }

        public IActionResult Privacy()
        {
            var patientLabTestKey = _addToCartEventTraking.GetCartIdentifier();

            ViewBag.PatientLabTestKey = patientLabTestKey;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
