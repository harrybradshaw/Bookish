using Bookish.Api.Models;

namespace Bookish.Api.Api
{
    public class LibraryHandler
    {
        private readonly BooksRepository _bookRepository = new();
        private readonly LoansRepository _loansRepository = new();
        private readonly UserRepository _userRepository = new();
        
        public bool Checkout(int userId, int bookId)
        {
            var book = _bookRepository.GetBook(bookId);
            if (book.BookCopies - _loansRepository.OnLoanByBookId(bookId) >= 1)
            {
                _loansRepository.CheckoutBook(userId,bookId);
                return true;
            }

            return false;
        }

        public bool Checkin(int loadId)
        {
            return _loansRepository.CheckinBook(loadId);
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }
    }
}