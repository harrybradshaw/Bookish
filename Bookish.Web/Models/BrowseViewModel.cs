using Bookish.Api.Models;

namespace Bookish.Web.Models
{
    public class BrowseViewModel
    {
        public Books Books;
        public BrowseViewModel(Books books)
        {
            this.Books = books;
        }
    }
}