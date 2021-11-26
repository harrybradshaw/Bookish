using Bookish.Api.Api;
using Bookish.Web.Models;
using Bookish.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookish.Web.Controllers
{
    public class BookController : Controller
    {
        // GET
        public IActionResult Index()
        {
            LibraryHandler libraryHandler = new LibraryHandler();
            return View(libraryHandler);
        }
        
        
        [HttpGet("book/{bookId}")]
        public IActionResult GetBook(int bookId)
        {
            LibraryServices libraryServices = new LibraryServices();
            var book = libraryServices.GetBook(bookId);
            var loans = libraryServices.GetLoansByBook(book);
            var booksAuthor = libraryServices.GetBooksByAuthorNotInc(bookId,book.AuthorList);
            var bvm = new BookViewModel(book, loans, booksAuthor);
            return View(bvm);
        }

        
    }
}