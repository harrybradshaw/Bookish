using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Bookish.Api.Models;
using Dapper;

namespace Bookish.Api.Api
{
    public class LoansRepository
    {
        private readonly DbHelper _dbHelp = new();

        public List<Loan> GetAllActiveLoans()
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString = "SELECT * FROM [Loans] INNER JOIN Books B on B.bookID = Loans.bookID INNER JOIN Users U on U.userID = Loans.userID WHERE loanComplete = 0";
            return (List<Loan>) db.Query<Loan>(sqlString);
        }
        
        public List<Loan> GetActiveLoansByBookId(int bookId)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString = "SELECT * FROM [Loans] INNER JOIN Books B on B.bookID = Loans.bookID INNER JOIN Users U on U.userID = Loans.userID WHERE B.bookID = @BookId AND loanComplete = 0";
            return (List<Loan>) db.Query<Loan>(sqlString, new {@BookId = bookId});
        }
        
        public List<Loan> GetActiveLoansByUserId(int userId)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString = "SELECT * FROM [Loans] INNER JOIN Books B on B.bookID = Loans.bookID INNER JOIN Users U on U.userID = Loans.userID WHERE Loans.userID = @UserId AND loanComplete = 0";
            return (List<Loan>) db.Query<Loan>(sqlString, new {@UserId = userId});
        }

        public bool CheckoutBook(int userId, int bookId)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString =
                "INSERT INTO [Loans] ([bookID],[userID],[loanOutDate],[loanDueDate]) VALUES (@BookId,@userId,@LoanOut,@LoanIn)";
            var rowsAltered = db.Execute(sqlString,new { @BookId = bookId, @UserId = userId, @LoanOut = DateTime.Now, @LoanIn = DateTime.Now.AddDays(7)});
            return rowsAltered > 0;
        }

        public bool CheckinBook(int loanId)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString = "UPDATE [Loans] SET [loanComplete] = 1 WHERE [loanID] = @LoanId";
            var rowsAltered = db.Execute(sqlString, new {@LoanId = loanId});
            return rowsAltered > 0;
        }

        public int OnLoanByBookId(int bookId)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString = "SELECT * FROM [Loans] WHERE [bookID] = @BookId AND [loanComplete] = 0";
            return db.Query(sqlString, new {@BookId = bookId}).ToList().Count;
        }
        
    }
}