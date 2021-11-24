namespace Bookish.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            //library.PrintAllBooks();
            library.PrintAllLoans();
            library.CheckInBook(1);
            //library.PrintStockOf(4);
            //library.Checkout(1,4);
        }
    }
}