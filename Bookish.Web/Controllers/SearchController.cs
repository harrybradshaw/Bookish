using Bookish.Web.Models;
using Bookish.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookish.Web.Controllers
{
    public class SearchController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult Index([FromQuery] string text)
        {
            LibraryServices libraryServices = new LibraryServices();
            var books = libraryServices.SearchLibrary(text);
            SearchViewModel searchViewModel = new SearchViewModel(books,text);
            return View(searchViewModel);
        }
    }
}