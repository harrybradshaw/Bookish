using System;
using Bookish.Api.Api;
using Bookish.Api.Models;
using Bookish.ConsoleApp.Api;

namespace Bookish.ConsoleApp
{
    public class Library
    {
        private Books _books = new();
        private Loans _loans = new();
        private readonly BooksRepository _bookRepository = new();
        private readonly LoansRepository _loansRepository = new();

        public Library()
        {
            _books.BookList = _bookRepository.GetBooks();
            _loans.LoanList = _loansRepository.GetAllActiveLoans();
            foreach (var book in _books.BookList)
            {
                book.SetAuthorList(_bookRepository.GetAllAuthorsForBook(book.bookID));
            }
        }

        public void PrintAllBooks()
        {
            foreach (var book in _books.BookList)
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
                Console.WriteLine($"'{book.bookTitle}' - {tempString} [{book.BookCopies} copies / {_loans.OnLoan(book.bookID)} on loan]");
            }
        }

        public void Checkout(int userId, int bookId)
        {
            var book = _books.GetBookById(bookId);
            if (book.BookCopies - _loans.OnLoan(bookId) >= 1)
            {
                _loans.ProcessLoan(userId,bookId);
            }
            else
            {
                Console.WriteLine("No copies available!");
            }
        }
        public void PrintAllLoans()
        {
            _loans.PrintAllLoans();
        }

        public void PrintStockOf(int bookId)
        {
            var book = _books.GetBookById(bookId);
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
            {
                
            }
        }

        public void Checkin(int loanId)
        {
            _loans.CheckInByLoanId(loanId);
        }
    }
}