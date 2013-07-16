using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace SearchByGivenString
{
    internal class SearchByString
    { 
        //my database is .\SLEXPRESS\NW
        //CHOOSE yours
        const string ServerExpress = ".\\SQLEXPRESS";
        const string NW = "NW";

        //sql server - not express
        const string ServerSQL = ".";
        const string NORTHWIND = "Northwind";

        static void Main()
        {
            var nortwindDb = new SqlConnection("Server=" + ServerExpress +
                ";Database=" + NW + ";Integrated Security=true;");
            
            nortwindDb.Open();

            using (nortwindDb)
            {
                Console.WriteLine("Give me search string:");
                var searchString = Console.ReadLine();

                var regEx = new Regex("(['%\"_])");

                var cleanedSearchString = regEx.Replace(searchString, "|$1");

                var searchCommand = new SqlCommand("SELECT ProductName FROM Products WHERE ProductName Like Concat('%', @searchStr, '%') ESCAPE '|'", nortwindDb);

                searchCommand.Parameters.AddWithValue("@searchStr", cleanedSearchString);

                var reader = searchCommand.ExecuteReader();

                var counter = 0;
                using (reader)
                {
                    while (reader.Read())
                    {
                        var name = reader[0];
                        Console.WriteLine(name);
                        counter++;
                    }
                }

                Console.WriteLine(counter + "Rows");
            }
        }
    }
}
