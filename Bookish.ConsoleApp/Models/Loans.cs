using System;
using System.Collections.Generic;
using Bookish.ConsoleApp.Api;

namespace Bookish.ConsoleApp.Models
{
    public class Loans
    {
        public List<Loan> LoanList;
        private LoansRepository _loansRepository = new LoansRepository();

        public Loans()
        {
            LoanList = _loansRepository.GetAllActiveLoans();
        }

        public bool ProcessLoan(int userId, int bookId)
        {
            UpdateLoans();
            return _loansRepository.CheckoutBook(userId, bookId);
        }

        public void UpdateLoans()
        {
            LoanList = _loansRepository.GetAllActiveLoans();
        }
        
        public void PrintAllLoans()
        {
            UpdateLoans();
            Console.WriteLine("---------------------");
            Console.WriteLine("Summary of all loans");
            Console.WriteLine("---------------------");
            int i = 1;
            foreach (var loan in LoanList)
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

        public int OnLoan(int bookId)
        {
            UpdateLoans();
            return _loansRepository.OnLoan(bookId);
        }

        public bool CheckIn(int loanId)
        {
            UpdateLoans();
            return _loansRepository.CheckinBook(loanId);
        }
    }
}