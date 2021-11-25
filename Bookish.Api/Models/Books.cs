using System.Collections.Generic;
using Bookish.Api.Api;


namespace Bookish.Api.Models
{
    public class Books
    {
        public List<Book> BookList;
        private readonly BooksRepository _booksRepository = new BooksRepository();

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

        public void UpdateBooks()
        {
            //Probably better to reduce overhead and not do a full call here. An optimisation for later. 
            BookList = _booksRepository.GetBooks();
            foreach (var book in BookList)
            {
                book.SetAuthorList(_booksRepository.GetAllAuthorsForBook(book.bookID));
            }
        }
    }
}