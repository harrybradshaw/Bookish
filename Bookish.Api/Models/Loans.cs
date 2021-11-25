using System;
using System.Collections.Generic;
using Bookish.Api.Api;

namespace Bookish.Api.Models
{
    public class Loans
    {
        public List<Loan> LoanList;
        private LoansRepository _loansRepository = new LoansRepository();

        public Loans()
        {
            LoanList = _loansRepository.GetAllActiveLoans();
        }

        public Loans(Book book)
        {
            LoanList = _loansRepository.GetActiveLoansByBookId(book.bookID);
        }

        public Loans(User user)
        {
            LoanList = _loansRepository.GetActiveLoansByUserId(user.UserID);
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