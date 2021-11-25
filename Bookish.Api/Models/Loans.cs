using System;
using System.Collections.Generic;
using Bookish.Api.Api;

namespace Bookish.Api.Models
{
    public class Loans
    {
        public List<Loan> LoanList;
        private LoansRepository _loansRepository = new LoansRepository();
        private BooksRepository _booksRepository = new BooksRepository();

        public Loans()
        {
            LoanList = _loansRepository.GetAllActiveLoans();
            foreach (var loan in LoanList)
            {
                var book = _booksRepository.GetBook(loan.BookId);
                book.SetAuthorList(_booksRepository.GetAllAuthorsForBook(loan.BookId));
                loan.book = book;
            }
        }

        public Loans(Book book)
        {
            LoanList = _loansRepository.GetActiveLoansByBookId(book.bookID);
            foreach (var loan in LoanList)
            {
                loan.book = book;
            }
        }

        public Loans(User user)
        {
            LoanList = _loansRepository.GetActiveLoansByUserId(user.UserID);
            foreach (var loan in LoanList)
            {
                var book = _booksRepository.GetBook(loan.BookId);
                book.SetAuthorList(_booksRepository.GetAllAuthorsForBook(loan.BookId));
                loan.book = book;
            }
        }
        
        public bool ProcessLoan(int userId, int bookId)
        {
            //UpdateLoans();
            return _loansRepository.CheckoutBook(userId, bookId);
        }

        public void UpdateLoans()
        {
            LoanList = _loansRepository.GetAllActiveLoans();
        }

        public void GetLoansByBook(int bookId)
        {
            LoanList = _loansRepository.GetActiveLoansByBookId(bookId);
        }

        public int OnLoan(int bookId)
        {
            //UpdateLoans();
            return _loansRepository.OnLoanByBookId(bookId);
        }

        public bool CheckInByLoanId(int loanId)
        {
            //UpdateLoans();
            return _loansRepository.CheckinBook(loanId);
        }
    }
}