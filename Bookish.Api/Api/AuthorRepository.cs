using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Bookish.Api.Models;

namespace Bookish.Api.Api
{
    public class AuthorRepository
    {
        private readonly DbHelper _dbHelp = new();
        
        public int AddAuthor(string authorName)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString =
                @"INSERT INTO [Authors] ([authorName]) VALUES (@AuthorName); 
                SELECT CAST(SCOPE_IDENTITY() as int)";
            var authorId = db.QuerySingle<int>(sqlString,new { @AuthorName = authorName});

            return authorId;
        }

        public int AuthorExists(string authorName)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString = "SELECT * FROM [Authors] WHERE authorName = @AuthorName";
            try
            {
                var authorId = db.Query<Author>(sqlString, new {@AuthorName = authorName}).Single().authorID;
                return authorId;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public void AddAuthorBook(int authorId, int bookId)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString = "INSERT INTO [AuthorBook] (bookID, authorID) VALUES (@BookId,@AuthorId)";
            var rows = db.Execute(sqlString, new {@BookId = bookId, @AuthorId = authorId});
        }
    }
}