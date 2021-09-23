using Aplicacao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sentry;
using System;
using System.Diagnostics;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHub _sentryHub;

        public HomeController(ILogger<HomeController> logger, IHub sentryHub)
        {
            _logger = logger;
            _sentryHub = sentryHub;
        }

        [HttpGet]
        public IActionResult Sentry()
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("additional-work");

            try
            {
                childSpan?.Finish(SpanStatus.Ok);
                throw new Exception();
            }
            catch (Exception e)
            {
                childSpan?.Finish(e);
                return RedirectToAction(nameof(Index));
            }
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
    }
}