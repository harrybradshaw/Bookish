using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bookish.Api.Api;
using Bookish.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bookish.Web.Models;
using Bookish.Web.Services;
using Microsoft.AspNetCore.Identity;

namespace Bookish.Web.Controllers
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
            LibraryServices libraryServices = new LibraryServices();
            var currentUserId = 1;
            Loans loans = libraryServices.GetLoansByUserId(currentUserId);
            HomeViewModel homeViewModel = new HomeViewModel(loans);
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult ReturnBook(int loanId)
        {
            LibraryServices libraryServices = new LibraryServices();
            libraryServices.Checkin(loanId);
            return RedirectToAction("Index");
        }
    }
}