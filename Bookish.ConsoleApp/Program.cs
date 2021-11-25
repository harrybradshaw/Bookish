using System.Linq;
using Bookish.Api.Models;

namespace Bookish.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            library.PrintAllBooks();
            //library.Checkout(2,3);
            //library.PrintAllLoans();
            // var user = library.GetUserById(1);
            // var loans = new Loans(user);
            // library.Checkin(loans.LoanList[0].LoanId);
            // library.PrintLoansByUser(user.UserID);
        }
    }
}