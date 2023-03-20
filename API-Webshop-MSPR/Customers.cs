using System.Net;

namespace API_Webshop_MSPR
{
    public class Customers
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public AdressCustomers Address { get; set; }
        public ProfileCustomers Profile { get; set; }
        public CompanyCustomers Company { get; set; }
        public List<Order> Orders { get; set; }
    }
}
