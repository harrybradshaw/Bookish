using Bookish.Api.Models;
using Bookish.Api.Api;

namespace Bookish.Web.Services
{
    public class LibraryServices
    {
        private readonly BooksRepository _booksRepository = new BooksRepository();
        private readonly LoansRepository _loansRepository = new();
        private readonly UserRepository _userRepository = new();
        public Books GetBooks()
        {
            var books = new Books();
            foreach (var book in books.BookList)
            {
                int copies = _loansRepository.OnLoanByBookId(book.bookID);
                book.SetCopiesLeft(copies);
            }
            return books;
        }

        public Book GetBook(int bookId)
        {
            var book = _booksRepository.GetBook(bookId);
            book.SetAuthorList(_booksRepository.GetAllAuthorsForBook(bookId));
            return book;
        }

        public Loans GetLoansByBook(Book book)
        {
            Loans loans = new Loans(book);
            return loans;
        }

        public Loans GetLoansByUserId(int userId)
        {
            User user = _userRepository.GetUserById(userId);
            Loans loans = new Loans(user);
            return loans;
        }
        
        public bool Checkout(int userId, int bookId)
        {
            var book = _booksRepository.GetBook(bookId);
            if (book.BookCopies - _loansRepository.OnLoanByBookId(bookId) >= 1)
            {
                _loansRepository.CheckoutBook(userId,bookId);
                return true;
            }

            return false;
        }
        
        public bool Checkin(int loanId)
        {
            return _loansRepository.CheckinBook(loanId);
        }

        public Books SearchLibrary(string searchString)
        {
            Books books = new Books();
            books.BookList = _booksRepository.SearchBooks(searchString);
            foreach (var book in books.BookList)
            {
                book.SetAuthorList(_booksRepository.GetAllAuthorsForBook(book.bookID));
            }
            return books;
        }
        
    }
}