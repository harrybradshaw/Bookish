namespace Bookish.Api.Api
{
    public class DbHelper
    {
        private string connectionString = "Server=localhost;database=Bookish;Trusted_Connection=True;";

        public string GetString()
        {
            return connectionString;
        }
            
    }
}