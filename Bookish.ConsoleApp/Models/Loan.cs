using System;

namespace Bookish.ConsoleApp.Models
{
    public class Loan
    {
        public int LoanId;
        public int BookId;
        public bool LoanComplete;
        public DateTime LoanOutdate;
        public DateTime LoanDuedate;
        public string UserName;
        public string BookTitle;
    }
}