using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.CopyNorthwind
{
    class CopyNorthwind
    {
        static void Main(string[] args)
        {
            using(var northwindDbCurrent = new Northwind.Models.NorthwindDb())
            {
                var script = ((IObjectContextAdapter)northwindDbCurrent).ObjectContext.CreateDatabaseScript();
                northwindDbCurrent.Database.ExecuteSqlCommand("USE master\nCREATE DATABASE NorthwindTwin ");
                script = "\n USE NorthwindTwin \n" + script;

                northwindDbCurrent.Database.ExecuteSqlCommand(script);            
            }
        }
    }
}
