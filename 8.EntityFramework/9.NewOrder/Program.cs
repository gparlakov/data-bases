using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Models;
using System.Data;

namespace _9.NewOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var northWind = new NorthwindDb())
            {
                //Task 9 -- throws exception which rolls back transaction
                //AddNewOrder(northWind);


                //Task 10
                CreateStoredProcedure(northWind);

                var sales = GetSupplierSales(northWind, "t",
                    new DateTime(1996, 1, 1), new DateTime(1997, 1, 1));

                Console.WriteLine("{0,-40} | {1,15}", "Name", "Sales");
                Console.WriteLine(new string('-', 58));
                foreach (var sale in sales)
                {
                    Console.WriteLine("{0,-40} | {1,15}",sale.Supplier, sale.Sales);
                }                
            }
        }
  
        private static IEnumerable<SupplierSalesResult> GetSupplierSales(
            NorthwindDb northWind, 
            string supplierName, 
            DateTime startDate, 
            DateTime endDate)
        {
            var sales = northWind.Database
                .SqlQuery<SupplierSalesResult>("EXEC usp_TotalIncomeForSupplier {0}, {1}, {2}",
                    supplierName, startDate, endDate);
            return sales;
        }
  
        private static void CreateStoredProcedure(NorthwindDb northWind)
        {
            northWind.Database.ExecuteSqlCommand("IF OBJECT_ID('usp_TotalIncomeForSupplier') IS NOT NULL DROP PROC usp_TotalIncomeForSupplier");
            

            northWind.Database.ExecuteSqlCommand(
                @"CREATE PROC usp_TotalIncomeForSupplier( 
  	                @supplier nvarchar(50),
  	                @startDate date,
  	                @endDate date)
                AS
                SELECT max(s.CompanyName) as Supplier,
  	                SUM(od.UnitPrice * od.Quantity) as Sales
                FROM NORTHWIND.dbo.Suppliers s
  	                JOIN Products p ON s.SupplierID = p.SupplierID
  	                JOIN [Order Details] od ON p.ProductID = od.ProductID
  	                JOIN Orders o ON od.OrderID = o.OrderID
                WHERE s.CompanyName LIKE CONCAT('%', @supplier, '%')
  	                AND o.OrderDate > @startDate 
  	                AND o.OrderDate < @endDate
                GROUP BY s.CompanyName");     
        }
  
        private static void AddNewOrder(NorthwindDb northWind)
        {
            var newOrder = new Order
            {
                Order_Details = new List<Order_Detail>
                {
                    new Order_Detail
                    {
                        UnitPrice = 2,
                        ProductID = 11,
                        Quantity = 16
                    },
                    new Order_Detail
                    {
                        UnitPrice = 12,
                        ProductID = 10,
                        Quantity = 5
                    },
                    new Order_Detail
                    {
                        UnitPrice = 14,
                        ProductID = -34, // not allowed - will 
                        Quantity = 5
                    },
                },
                OrderDate = DateTime.Now,
                ShipCountry = "Bulgaria"
            };

            northWind.Orders.Add(newOrder);
            northWind.SaveChanges();
        }

        internal class SupplierSalesResult
        {
            public string Supplier { get; set; }
            public decimal Sales { get; set; }
        }
    }
}
