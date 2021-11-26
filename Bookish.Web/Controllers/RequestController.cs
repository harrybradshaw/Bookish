using Bookish.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookish.Web.Controllers
{
    public class RequestController : Controller
    {
        // GET
        
        public IActionResult Index([FromQuery] string bookTitle, [FromQuery] string bookAuthors, [FromQuery] string bookDesc)
        {
            if (bookTitle != null)
            {
                LibraryServices libraryServices = new LibraryServices();
                libraryServices.AddBookFromRequest(bookTitle, bookAuthors, bookDesc);
                return RedirectToAction("Index", "Browse");
            }
            return View();
            
            
        }
    }
}