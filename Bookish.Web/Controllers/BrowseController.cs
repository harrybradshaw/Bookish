using Bookish.Api.Models;
using Bookish.Web.Models;
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
            BrowseViewModel bvm = new BrowseViewModel(books);
            return View(bvm);
        }
        public IActionResult CheckoutBook(int bookId)
        {
            LibraryServices libraryServices = new LibraryServices();
            libraryServices.Checkout(1,bookId);
            return RedirectToAction("Index");
        }



    }
}