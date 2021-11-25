using System;

namespace Bookish.Api.Models
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
        public int UserId;
    }
}