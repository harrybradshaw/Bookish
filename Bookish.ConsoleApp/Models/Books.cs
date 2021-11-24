using System.Collections.Generic;
using Bookish.ConsoleApp.Api;

namespace Bookish.ConsoleApp.Models
{
    public class Books
    {
        public List<Book> BookList;
        private BooksRepository _booksRepository = new BooksRepository();

        public Books()
        {
            BookList = _booksRepository.GetBooks();
        }

        public Book GetBookById(int bookId)
        {
            foreach (var book in BookList)
            {
                if (book.bookID == bookId)
                {
                    return book;
                }
            }
            return null;
        }
    }
}