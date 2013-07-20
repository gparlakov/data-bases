using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Models;
using System.Data;
using System.Data.Linq;

namespace _8.EmployeeUpgrade
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var northDb = new NorthwindDb())
            {
                //Test with include = only one query
                var employees = northDb.Employees.Include("Territories").Where(x => x.Country == "UK");
                
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.Territories.First().TerritoryDescription);
                }

                //test with upgraded emplyee - n + 1 queries
                var employeesUS = northDb.Employees.Where(em => em.Country == "USA");

                foreach (var empl in employeesUS)
                {
                    Console.WriteLine(empl.EntityTerritories.First().TerritoryDescription);
                }
            }
        }
    }    
}
