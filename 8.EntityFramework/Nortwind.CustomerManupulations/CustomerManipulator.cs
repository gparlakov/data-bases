using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nortwind.CustomerManupulations
{
    public static class CustomerManipulator
    {
        #region Task2 region
        /// <summary>
        /// Inserts given Customer instance in db as long as CustomerID is 5 symbols long 
        /// and is not in database already. And Company Name is not Null
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static bool Insert(Customer customer, NorthwindDb nortwindDb)
        {
            bool inserted = false;

            ValidateCustomerIstance(customer);

            nortwindDb.Customers.Add(customer);
            if (nortwindDb.SaveChanges() == 1)
            {
                inserted = true;
            }

            return inserted;
        }

        /// <summary>
        /// Sets the properties of given object to the object with given customerID fetched from database.
        /// if <paramref name="setOmittedValuesToNull"/> is set to true it will set all values not specified
        /// in the new object to null i.e. if newCustomer{Address = "new Address"} all other porperties:
        /// CompanyName, ContactName .... will be set to NULL except for 
        /// </summary>
        /// <param name="customerID">The object from database will be fetched using this string CustomerID</param>
        /// <param name="newCustomer">The properties will bi taken from this object of type Customer</param>
        /// <param name="setOmittedValuesToNull">if set to true will put null on all omitted (not set) values in the <paramref name="newCustomer"/></param>
        /// <returns>true if modify opperation is successful</returns>
        public static bool Modify(string customerID, Customer newCustomer, NorthwindDb nortwindDb, bool setOmittedValuesToNull = false)
        {
            bool modified = false;

            var customer = nortwindDb.Customers.FirstOrDefault(x => x.CustomerID == customerID);

            // this loop iterates through the properties of customer e.g. CustomerId, name, company name,
            // ...etc. and change them to values of given object
            foreach (var property in newCustomer.GetType().GetProperties())
            {
                var newValue = property.GetValue(newCustomer);

                if (newValue == null && !setOmittedValuesToNull)
                {
                    continue;
                }

                customer.GetType().GetProperty(property.Name).SetValue(customer, newValue);
            }

            ValidateCustomerIstance(customer);

            if (nortwindDb.SaveChanges() == 1)
            {
                modified = true;
            }

            return modified;
        }

        public static bool Delete(string customerID, NorthwindDb nortwindDb)
        {
            var deleted = false;

            var customerToDelete = nortwindDb.Customers.FirstOrDefault(x => x.CustomerID == customerID);
            if (customerToDelete == null)
            {
                throw new KeyNotFoundException("No such customer is found in database");
            }

            nortwindDb.Customers.Remove(customerToDelete);

            if (nortwindDb.SaveChanges() == 1)
            {
                deleted = true;
            }

            return deleted;
        }

        /// <summary>
        /// Prints the known properties of the given instance of customer
        /// Will skip over the ICollection types because they need open connection 
        /// to database to be fetched from other tables - like NORTHWIND.Orders and so on
        /// </summary>
        /// <param name="customerID">Id to search customer by</param> 
        #endregion
        
        #region Task 3 region

        public static ICollection<Customer>
            GetCustomerByOrderYearShippingCountry(
            int year, string country, NorthwindDb nortwindDb)
        {
            if (year < 1990 || year > 2050)
            {
                throw new ArgumentException("Year should be between 1990 and 2050");
            }

            var customers = nortwindDb.Customers
                .Join(nortwindDb.Orders,
                    cust => cust.CustomerID,
                    ord => ord.CustomerID,
                    (cust, ord) => new
                    {
                        CustomerId = cust.CustomerID,
                        Year = ord.OrderDate.Value.Year,
                        Country = ord.ShipCountry
                    })
                .Where(selection => selection.Country == country && selection.Year == year)
                .GroupBy(selection => selection.CustomerId)
                .Join(nortwindDb.Customers,
                    sel => sel.Key,
                    cust => cust.CustomerID,
                    (selection, cust) => cust)
                .ToList();

            return customers;
        }

        #endregion

        #region Task 5 Region
        public static ICollection<SalesInRegionSearchResult> FindSalesSumByRegionAndDateInterval(
            string region, DateTime startDate, DateTime endDate, Northwind.Models.NorthwindDb dbContext)
        {
            //because if found (below) is null and we try to convert it to SalesInRegionSearchResult type and get an argumentNullException            
            if (!RegionExists(region, dbContext))
            {
                return new List<SalesInRegionSearchResult>();
            }

            var found = dbContext.Database.SqlQuery(typeof(SalesInRegionSearchResult),
                @"SELECT SUM(od.Quantity*od.UnitPrice) as Sales,
	                    max(r.RegionDescription) as Region
                FROM Orders o 
	                JOIN Employees e			ON o.EmployeeID = e.EmployeeID
	                JOIN EmployeeTerritories et ON e.EmployeeID = et.EmployeeID
	                JOIN Territories t			ON t.TerritoryID = et.TerritoryID
	                JOIN Region r				ON r.RegionID = t.RegionID
	                JOIN [Order Details] od		ON od.OrderID = o.OrderID
	                JOIN Products p				ON p.ProductID = od.ProductID
                WHERE o.OrderDate > {1} 
	                AND o.OrderDate < {2}
	                AND r.RegionDescription = {0}",          
            region, startDate, endDate);
            
            var foundToList = ConvertToList(found);

            return foundToList;     
        }

        private static bool RegionExists(string region, NorthwindDb dbContext)
        {
            var exists = false;

            var regions = dbContext.Regions.Select(x => x.RegionDescription).ToList();

            foreach (var reg in regions)
            {
                if (reg.Trim() == region)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }

        private static List<SalesInRegionSearchResult> ConvertToList(System.Collections.IEnumerable found)
        {
            var foundToList = new List<SalesInRegionSearchResult>();

            foreach (var item in found)
            {
                foundToList.Add((SalesInRegionSearchResult)item);
            }
            return foundToList;
        }

        #endregion

        #region Print customer on console
        /// <summary>
        /// Prints the known properties of the given instance of customer
        /// Will skip over the ICollection types because they need open connection 
        /// to database to be fetched from other tables - like NORTHWIND.Orders and so on
        /// </summary>
        /// <param name="customer">Instance of customer-like object....</param>
        public static void Print(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer", "Can not be null!!");
            }

            var customerString = new StringBuilder();

            var customerCopy = (Customer)customer;

            foreach (var property in customerCopy.GetType().GetProperties())
            {
                //skips over the information that has to be taken from database 
                //bacause currently the connection has been terminated
                var type = property.PropertyType.Name;

                if (type.StartsWith("I") && !type.StartsWith("INT"))
                {
                    continue;
                }
                var value = property.GetValue(customer) ?? "UNKNOWN";
                customerString.AppendFormat("|| {0} : {1}\n", property.Name, value);
            }

            Console.WriteLine(customerString.ToString());
        }

        public static void Print(string customerID, NorthwindDb nortwindDb)
        {
            var customer = nortwindDb.Customers.FirstOrDefault(x => x.CustomerID == customerID);
            if (customer == null)
            {
                Console.WriteLine("No such customer is found in database");
            }
            else
            {
                var customerString = new StringBuilder();

                foreach (var property in customer.GetType().GetProperties())
                {
                    //skips over the information that has to be taken from database 
                    //bacause currently the connection has been terminated
                    var type = property.PropertyType.Name;
                    if (type.StartsWith("I") && !type.StartsWith("INT"))
                    {
                        continue;
                    }

                    var value = property.GetValue(customer);
                    customerString.AppendFormat("|| {0} : {1}\n", property.Name, value);
                }
                Console.WriteLine(customerString.ToString());
            }

        }
        
        #endregion

        public static void PrintExceptionOnConsole(Exception ex)
        {
            if (ex.Message != null)
            {
                Console.WriteLine(ex.Message);
            }

            if (ex.InnerException.Message != null)
            {
                Console.WriteLine(ex.InnerException.Message);
            }

            if (ex.InnerException.InnerException.Message != null)
            {
                Console.WriteLine(ex.InnerException.InnerException.Message);
            }
        }

        #region Private methods
        private static void ValidateCustomerIstance(Customer customer)
        {
            if (customer.CustomerID == null)
            {
                throw new ArgumentNullException("CustomerID", "CustomerId can not be null!");
            }

            if (customer.CustomerID.Length != 5)
            {
                throw new ArgumentException("CustomerID must be a 5 symbol string and not");
            }

            if (customer.CompanyName == null)
            {
                throw new ArgumentNullException("CompanyName", "Company name can not be null!");
            }

            if (customer.CompanyName.Length > 40)
            {
                throw new ArgumentException("Company name can not be longer than 40 symbols!");
            }
        } 
        #endregion
    }
}
