using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.SearchCustomersNativeSQLThroughEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var telerikDbContext = new NorthwindEntity.Model.NorthwindEntity() )
            {
                var customerNames = telerikDbContext.Database.
                    SqlQuery<string>(
                    "SELECT DISTINCT(c.CompanyName) " +
                    "FROM Customers c JOIN Orders o " +
                    "ON c.CustomerID = o.CustomerID " +
                    "WHERE o.OrderDate like '%1997%' AND o.ShipCountry = 'Canada'");

                foreach (var cust in customerNames)
                {
                    Console.WriteLine(cust);
                }
            }
        }
    }
}
