using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace Nortwind.CustomerManupulations
{
    public class CustomersDemo
    {

        static void Main(string[] args)
        {
            

            // Task 1!!
            using (var dbContext = new Northwind.Models.NorthwindDb())
            {
                //..code here   

                try
                {
                    var customer = new Customer
                    {
                        ContactName = "new Customer",
                        Address = "Sofia",
                        CompanyName = "Telerik Academy",
                        CustomerID = "TELER"
                    };

                    //task2 
                    InsertModifyDeleteCustomer(customer, dbContext);

                    //task 3
                    FindByOrderYearAndDestinationCountry(1997, "Canada", dbContext);

                    //task 5
                    var found = CustomerManipulator.FindSalesSumByRegionAndDateInterval(
                        region: "Western",
                        startDate: new DateTime(1996, 1, 1),
                        endDate: new DateTime(1996, 12, 31),
                        dbContext: dbContext);
                                        
                    foreach (var item in found)
                    {
                        Console.WriteLine("The sum of sales in region {0} is {1}", item.Region.Trim(), item.Sales);
                    }

                }
                catch (Exception ex)
                {
                    CustomerManipulator.PrintExceptionOnConsole(ex);
                }
            }

        }

        /// <summary>
        /// Create a DAO class with static methods which provide functionality 
        /// for inserting, modifying and deleting customers. Write a testing class.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="dbContext"></param>
        private static void InsertModifyDeleteCustomer(Customer customer, NorthwindDb dbContext)
        {
            Console.WriteLine("Inserted {0}", CustomerManipulator.Insert(customer, dbContext));
            CustomerManipulator.Print(customer);

            var modified = CustomerManipulator.Modify("TELER", new Customer { Address = "55 Mallinov Blvd." }, dbContext);
            Console.WriteLine("The customer was modified => {0}", modified);
            CustomerManipulator.Print(customer);

            var deleted = CustomerManipulator.Delete("TELER", dbContext);
            Console.WriteLine("TELER deleted => {0}", deleted);
            CustomerManipulator.Print(customer);
        }

        /// <summary>
        /// Create a DAO class with static methods which provide functionality 
        /// for inserting, modifying and deleting customers. Write a testing class.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="country"></param>
        private static void FindByOrderYearAndDestinationCountry(int year, string country, NorthwindDb dbContext)
        {
            var customers = CustomerManipulator
                .GetCustomerByOrderYearShippingCountry(year, country, dbContext);

            Console.WriteLine("There are {0} customers that have orders from "+
                "{1} and shipped to {2}:\nClick :) to show...\n",
                customers.Count, year, country);
            Console.ReadLine();
            foreach (var cust in customers)
            {
                CustomerManipulator.Print(cust);
            }
        }
    }
}
