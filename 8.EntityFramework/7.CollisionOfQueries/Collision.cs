using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace _7.CollisionOfQueries
{
    public class Collision
    {
        static void Main(string[] args)
        {
            var firstContext = new NorthwindDb();           
            var firstEmployee = firstContext.Employees.First();
           
            var secondContext = new NorthwindDb();            
            var firstEmpl = secondContext.Employees.First();

            var firstName = firstEmployee.FirstName;
            firstEmployee.FirstName = firstName + 1;

            firstEmpl.FirstName = firstName + 2;

            firstContext.SaveChanges();

            //throws exception - because of concurency 
            secondContext.SaveChanges();            
        }
    }
}
