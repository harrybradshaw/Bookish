using System;
using Bookish.Api.Api;
using Bookish.Api.Models;

namespace Bookish.ConsoleApp
{
    public class Library
    {
        private readonly BooksRepository _bookRepository = new();
        private readonly LoansRepository _loansRepository = new();
        private readonly LibraryHandler _libraryHandler = new();
        private readonly UserRepository _userRepository = new();

        public Library()
        {
            //_books.BookList = _bookRepository.GetBooks();
            //_loans.LoanList = _loansRepository.GetAllActiveLoans();
            //foreach (var book in _books.BookList)
            //{
            //    book.SetAuthorList(_bookRepository.GetAllAuthorsForBook(book.bookID));
            //}
        }

        public void Checkout(int userId, int bookId)
        {
            if (!_libraryHandler.Checkout(userId, bookId))
            {
                Console.WriteLine("No copies to checkout!");
            }
        }
        public void PrintAllBooks()
        {
            var books = new Books();
            Console.WriteLine("The library has...");
            foreach (var book in books.BookList)
            {
                int i = 0;
                var tempString = "";
                foreach (var author in book.AuthorList)
                {
                    tempString += author.authorName;
                    if (i > 0)
                    {
                        tempString += ", ";
                    }
                    i++;
                }
                Console.WriteLine($"'{book.bookTitle}' - {tempString} [{book.BookCopies} copies / {_loansRepository.OnLoanByBookId(book.bookID)} on loan] ({book.BookCoverString})");
            }
        }
        
        public void PrintAllLoans()
        {
            var loans = new Loans();
            Console.WriteLine("---------------------");
            Console.WriteLine("Summary of all loans");
            Console.WriteLine("---------------------");
            int i = 1;
            foreach (var loan in loans.LoanList)
            {
                Console.WriteLine($"{i}: {loan.BookTitle} on loan by {loan.UserName} from {loan.LoanOutdate.ToShortDateString()} to {loan.LoanDuedate.ToShortDateString()}");
                i++;
            }

            if (i == 1)
            {
                Console.WriteLine("No books on loan!");
            }
            Console.WriteLine("---------------------");
        }

        public void PrintStockOf(int bookId)
        {
            var book = _bookRepository.GetBook(bookId);
            var thisLoans = new Loans(book);
            var stock = book.BookCopies - thisLoans.LoanList.Count;
            Console.WriteLine(book.bookTitle);
            Console.WriteLine($"{book.BookCopies} in circulation");
            Console.WriteLine($"{stock} copies available");
            Console.WriteLine("Loaned by:");
            foreach (var thisLoan in thisLoans.LoanList)
            {
                Console.WriteLine($"{thisLoan.UserName} due {thisLoan.LoanDuedate}");
            }
        }

        public void Checkin(int loanId)
        {
            _libraryHandler.Checkin(loanId);
        }

        public void PrintLoansByUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            var loans = new Loans(user);
            Console.WriteLine($"Loans for {user.UserName}:");
            foreach (var loan in loans.LoanList)
            {
                Console.WriteLine($"'{loan.BookTitle}' due {loan.LoanDuedate}");
            }
        }

        public User GetUserById(int userId)
        {
            return _libraryHandler.GetUserById(userId);
        }
    }
}