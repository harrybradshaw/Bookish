using Bookish.Api.Models;

namespace Bookish.Web.Models
{
    public class BookViewModel
    {
        public Book book;
        public Loans loans;
        public int CopiesRemaining;
        public int CopiesLoaned;

        public BookViewModel(Book book, Loans loans)
        {
            this.book = book;
            this.loans = loans;
            CopiesRemaining = this.book.BookCopies - this.loans.LoanList.Count;
            CopiesLoaned = this.loans.LoanList.Count;
        }
    }
}