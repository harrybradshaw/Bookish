using Bookish.Api.Models;

namespace Bookish.Web.Models
{
    public class HomeViewModel
    {
        public Loans Loans;
        public Books books;

        public HomeViewModel(Loans loans)
        {
            this.Loans = loans;
        }
    }
}