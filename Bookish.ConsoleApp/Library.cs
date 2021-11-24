using System;
using System.Collections.Generic;
using Bookish.ConsoleApp.Api;
using Bookish.ConsoleApp.Models;

namespace Bookish.ConsoleApp
{
    public class Library
    {
        private Books _books = new();
        private Loans _loans = new();
        private readonly BooksRepository _bookRepository = new ();
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

        public void UpdateBooks()
        {
            //Probably better to reduce overhead and not do a full call here. A fix for later. 
            _books.BookList = _bookRepository.GetBooks();
            foreach (var book in _books.BookList)
            {
                book.SetAuthorList(_bookRepository.GetAllAuthorsForBook(book.bookID));
            }
        }

        public void PrintAllBooks()
        {
            UpdateBooks();
            foreach (var book in _books.BookList)
            {
                string tempString = "";
                int i = 0;
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
            var stock = book.BookCopies - _loans.OnLoan(book.bookID);
            Console.WriteLine(book.bookTitle);
            Console.WriteLine($"{book.BookCopies} in circulation");
            Console.WriteLine($"{stock} copies available");
        }

        public void CheckInBook(int loanId)
        {
            _loans.CheckIn(loanId);
        }
    }
}