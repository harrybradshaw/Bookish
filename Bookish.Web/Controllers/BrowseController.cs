using Bookish.Api.Models;
using Bookish.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookish.Web.Controllers
{
    public class BrowseController : Controller
    {
        // GET
        public IActionResult Index()
        {
            LibraryServices libraryServices = new LibraryServices();
            Books books = libraryServices.GetBooks();
            return View(books);
        }
    }
}