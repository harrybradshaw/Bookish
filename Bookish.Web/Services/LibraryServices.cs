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
            return new Books();
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
            var book = _booksRepository.GetBook(1);
            if (book.BookCopies - _loansRepository.OnLoanByBookId(bookId) >= 1)
            {
                _loansRepository.CheckoutBook(userId,bookId);
                return true;
            }

            return false;
        }
    }
}