using Bookish.Api.Models;

namespace Bookish.Web.Models
{
    public class SearchViewModel
    {
        public Books BooksFound;
        public string SearchString;

        public SearchViewModel(Books booksFound, string searchString)
        {
            this.BooksFound = booksFound;
            this.SearchString = searchString;
        }
    }
}