using System;
using System.Collections.Generic;
using Bookish.ConsoleApp.Api;
using Bookish.ConsoleApp.Models;

namespace Bookish.ConsoleApp
{
    public class Library
    {
        public List<Book> Books;
        public List<Loan> Loans;
        private readonly BooksRepository _bookRepository = new ();
        private readonly LoansRepository _loansRepository = new();

        public Library()
        {
            
            Books = _bookRepository.GetBooks();
            Loans = _loansRepository.GetAllActiveLoans();
            foreach (var book in Books)
            {
                book.SetAuthorList(_bookRepository.GetAllAuthorsForBook(book.bookID));
            }
        }

        public void UpdateBooks()
        {
            //Probably better to reduce overhead and not do a full call here. A fix for later. 
            Books = _bookRepository.GetBooks();
            foreach (var book in Books)
            {
                book.SetAuthorList(_bookRepository.GetAllAuthorsForBook(book.bookID));
            }
        }

        public void PrintAllBooks()
        {
            UpdateBooks();
            foreach (var book in Books)
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
                Console.WriteLine($"{book.bookTitle} - {tempString}");
            }
        }

        public void PrintAllLoans()
        {
            foreach (var loan in Loans)
            {
                Console.WriteLine($"{loan.LoanId}: {loan.BookTitle} on loan by {loan.UserName} from {loan.LoanOutdate} to {loan.LoanDuedate}");
            }
        }
    }
}