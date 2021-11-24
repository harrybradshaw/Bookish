using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Bookish.ConsoleApp.Models;
using Dapper;

namespace Bookish.ConsoleApp.Api
{
    public class LoansRepository
    {
        private DbHelper dbHelp = new DbHelper();

        public List<Loan> GetAllActiveLoans()
        {
            using IDbConnection db = new SqlConnection(dbHelp.GetString());
            var sqlString = "SELECT * FROM [Loans] INNER JOIN Books B on B.bookID = Loans.bookID INNER JOIN Users U on U.userID = Loans.userID WHERE loanComplete = 0";
            return (List<Loan>) db.Query<Loan>(sqlString);
        }
        
        public List<Loan> GetActiveLoansByBookId(int bookId)
        {
            using IDbConnection db = new SqlConnection(dbHelp.GetString());
            var sqlString = "SELECT * FROM [Loans] INNER JOIN Books B on B.bookID = Loans.bookID INNER JOIN Users U on U.userID = Loans.userID WHERE loanID = @LoanId AND loanComplete = 0";
            return (List<Loan>) db.Query<Loan>(sqlString);
        }
        
        public List<Loan> GetActiveLoansByUserId(int userId)
        {
            using IDbConnection db = new SqlConnection(dbHelp.GetString());
            var sqlString = "SELECT * FROM [Loans] INNER JOIN Books B on B.bookID = Loans.bookID INNER JOIN Users U on U.userID = Loans.userID WHERE Loans.userID = @UserId AND loanComplete = 0";
            return (List<Loan>) db.Query<Loan>(sqlString);
        }
        
    }
}