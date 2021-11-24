using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Bookish.ConsoleApp.Api;
using Dapper;

namespace Bookish.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BooksRepository booksRepository = new BooksRepository();
            Console.WriteLine(booksRepository.GetBook(1).bookTitle);
        }
    }
}