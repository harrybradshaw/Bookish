using System.Collections.Generic;
using Bookish.Api.Api;


namespace Bookish.Api.Models
{
    public class Books
    {
        public List<Book> BookList;
        private readonly BooksRepository _booksRepository = new();

        public Books()
        {
            BookList = _booksRepository.GetBooks();
            foreach (var book in BookList)
            {
                book.SetAuthorList(_booksRepository.GetAllAuthorsForBook(book.bookID));
            }
        }
    }
}