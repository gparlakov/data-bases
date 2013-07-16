using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTestAdo.NET
{
    internal class CategoriesProducts
    {
        //my database is .\SLEXPRESS\NW
        //CHOOSE yours
        const string ServerExpress = ".\\SQLEXPRESS";
        const string NW = "NW";

        //sql server - not express
        const string ServerSQL = ".";
        const string NORTHWIND = "Northwind";

        static void Main(string[] args)
        {
            SqlConnection northwindDb = new SqlConnection("Server=" + ServerExpress + ";Database=" + NW + ";Integrated Security=true;");
            northwindDb.Open();
            using (northwindDb)
            {   
                // task 1
                PrintNumberRows(northwindDb);
                AwaitClick("Retrieve the name and description of all categories in the Northwind DB.");

                // task 2
                PrintNamesDescriptions(northwindDb);
                AwaitClick("Retrieve product categories and the names of the products in each category");

                // task 3
                PrintProductsInCategories(northwindDb);
            }
        }

        private static void AwaitClick(string message)
        {
            Console.WriteLine("Press enter for nex task\n" + message);
            Console.ReadLine();
        }

        /// <summary>
        /// 3. Write a program that retrieves from the Northwind database all product categories and 
        /// the names of the products in each category. Can you do this with a single SQL query (with table join)?
        /// </summary>
        /// <param name="northwindDb"></param>
        private static void PrintProductsInCategories(SqlConnection northwindDb)
        {
            var productsInCategoriesCommand = new SqlCommand("SELECT p.ProductName, c.CategoryName " +
                "FROM Products p JOIN Categories c ON p.CategoryID = c.CategoryID ORDER BY p.CategoryID, p.ProductName",
                northwindDb);

            var productsCategoriesReader = productsInCategoriesCommand.ExecuteReader();

            var productsInCategorieBuilder = new StringBuilder();
            var currCategory = string.Empty;

            using (productsCategoriesReader)
            {
                while (productsCategoriesReader.Read())
                {
                    var product = (string)productsCategoriesReader["ProductName"];
                    var category = (string)productsCategoriesReader["CategoryName"];
                    if (category != currCategory)
                    {
                        productsInCategorieBuilder.AppendFormat("\n{0}\n", category.ToUpper());
                        currCategory = category;
                    }

                    productsInCategorieBuilder.AppendFormat("{0,35}\n", product);
                }
            }

            Console.WriteLine(productsInCategorieBuilder.ToString());
        }

        /// <summary>
        /// 2.Write a program that retrieves the name and description of all categories in the Northwind DB.
        /// </summary>
        /// <param name="northwindDb"></param>
        private static void PrintNamesDescriptions(SqlConnection northwindDb)
        {
            var nameDescriptionCategoriesCommand = 
                new SqlCommand("SELECT c.CategoryName,c.Description From Categories c");
            nameDescriptionCategoriesCommand.Connection = northwindDb;
            var namesDesctiptionsOfCategoriesReader = nameDescriptionCategoriesCommand.ExecuteReader();

            var resultBuilder = new StringBuilder();
            resultBuilder.AppendFormat("{0,-15} | {1,-25}\n{2}\n", "Name", "Description", new string('_',80));
            using (namesDesctiptionsOfCategoriesReader)
            {
                while (namesDesctiptionsOfCategoriesReader.Read())
                {
                    var name = (string)namesDesctiptionsOfCategoriesReader["CategoryName"];
                    var description = (string)namesDesctiptionsOfCategoriesReader["Description"];
                    resultBuilder.AppendFormat("{0,-15} | {1,-25}\n", name, description);
                }
            }
            Console.WriteLine(resultBuilder.ToString());
        }

        /// <summary>
        /// 1. Write a program that retrieves from the Northwind sample database in MS SQL Server the
        /// number of  rows in the Categories table.
        /// </summary>
        /// <param name="northwindDb"></param>
        private static void PrintNumberRows(SqlConnection northwindDb)
        {
            var numberRowsCommand = new SqlCommand("SELECT Count(*) FROM Categories");
            numberRowsCommand.Connection = northwindDb;
            var rowsNumber = (int)numberRowsCommand.ExecuteScalar();
            Console.WriteLine("Rows in table Categories {0}\n\n", rowsNumber);
        }
    }
}
