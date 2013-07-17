using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchThroughNativeSQL
{
    internal class SearchThroughNativeSQL
    {
        static void Main()
        {
            var telerikDbContext = new Northwind.Models.NorthwindDb();
            using (telerikDbContext)
            {
                var customerNames = telerikDbContext.Database.
                    SqlQuery<string>(
                    "SELECT DISTINCT(c.CompanyName) " +
                    "FROM Customers c JOIN Orders o " +
                    "ON c.CustomerID = o.CustomerID " +
                    "WHERE o.OrderDate like '%1997%' AND o.ShipCountry = 'Canada'");

                Console.WriteLine("There are {0} customers/companies that oredered in 1997 and shipped to Canada");
                
                foreach (var cust in customerNames)
                {
                    Console.WriteLine(cust);
                }
            }
        }
    }
}
