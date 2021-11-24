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
            Library library = new Library();
            //library.PrintAllBooks();
            library.PrintAllLoans();
        }
    }
}