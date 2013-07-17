using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindEntity.Model;

namespace Northwind.Demo
{
    public class NorthwindDemo
    {
        static void Main(string[] args)
        {
            // Task 1!!
            using (var dbContext = new NorthwindEntity.Model.NorthwindEntity())
            {
             //..code here   
            }

            var customer = new Customer
            {
                ContactName = "new Customer",
                Address = "Sofia",
                CompanyName = "Telerik Academy",
                CustomerID = "TELER"
            };

           // Task2Method(customer);

            Task3Method(1997, "Canada");
            
        }
        
        //not very HQC but explaines
        private static void Task2Method(Customer customer)
        {
            Console.WriteLine("Inserted {0}", CustomerManipulator.Insert(customer));
            CustomerManipulator.Print("TELER");

            var modified = CustomerManipulator.Modify("TELER", new Customer { Address = "55 Mallinov Blvd." });
            Console.WriteLine("The customer was modified => {0}", modified);
            CustomerManipulator.Print("TELER");

            var deleted = CustomerManipulator.Delete("TELER");
            Console.WriteLine("TELER deleted => {0}", deleted);
            CustomerManipulator.Print("TELER");
        }

        private static void Task3Method(int year, string country)
        {
            var customers = CustomerManipulator
                .GetCustomerByYearOfOrderAndShippingCountry(year, country);

            Console.WriteLine("There are {0} customers that have orders from {1} and shipped to {2}:\nClick :) to show...\n",
                customers.Count, year, country);
            Console.ReadLine();
            foreach (var cust in customers)
            {
                CustomerManipulator.Print(cust);
            }
        }
    }
}
