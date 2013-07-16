using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.AddProduct
{
    internal class AddProduct
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
            var nortwindDb = new SqlConnection(
                "Server=" + ServerExpress + ";Database=" + NW + ";Integrated Security=true;");
            nortwindDb.Open();
            using(nortwindDb)
	        {		
                var shpekSalam = new Product("Shpek Salam")
                {
                    SupplierId = 2,
                    CategoryId = 3,
                    QuantityPerUnit = "10 Palki",
                    UnitsInStock = 500,
                    UnitPrice = 25.65m,
                };

                //Trying to add this product will throw an exception due to non-existent supplierId/ categoryId
                var exceptionProduct = new Product("Shpek Salam")
                {
                    SupplierId = 2222,
                    CategoryId = 3333,
                    QuantityPerUnit = "10 Palki",
                    UnitsInStock = 500,
                    UnitPrice = 25.65m,
                };

                try
                {
                    var addProductCommand = AddNewProduct(shpekSalam);
                    addProductCommand.Connection = nortwindDb;
                    var executed = addProductCommand.ExecuteNonQuery();

                    Console.WriteLine("Rows affected: {0}",executed);
                    Console.WriteLine("Added new product with name: {0}", shpekSalam.Name);
                } 
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);               
                }
	        }   
        }

        private static SqlCommand AddNewProduct(Product newProduct)
        {
            var addProductCommand = new SqlCommand("INSERT INTO Products " +
                "VALUES (@productName, @supplierId, @categoryId, @quantityPerUnit," +
                " @unitPrice, @unitsInStock, @unitsOnOrder, @reorderLevel, @discontinued)");

            var suppId = ParameterAddValueOrNull("@supplierId", newProduct.SupplierId);
            var categoryId = ParameterAddValueOrNull("@categoryId", newProduct.CategoryId);
            var quantityPerUnit = ParameterAddValueOrNull("@quantityPerUnit", newProduct.QuantityPerUnit);
            var unitPrice = ParameterAddValueOrNull("@unitPrice", newProduct.UnitPrice);
            var unitsInStock = ParameterAddValueOrNull("@unitsInStock", newProduct.UnitsInStock);
            var unitsInOrder = ParameterAddValueOrNull("@unitsOnOrder", newProduct.UnitsInOrder);
            var reorderLevel = ParameterAddValueOrNull("@reorderLevel", newProduct.ReorderLevel);            

            addProductCommand.Parameters.AddWithValue("@productName", newProduct.Name);
            addProductCommand.Parameters.Add(suppId);
            addProductCommand.Parameters.Add(categoryId);
            addProductCommand.Parameters.Add(quantityPerUnit);
            addProductCommand.Parameters.Add(unitPrice);
            addProductCommand.Parameters.Add(unitsInStock);
            addProductCommand.Parameters.Add(unitsInOrder);
            addProductCommand.Parameters.Add(reorderLevel);
            addProductCommand.Parameters.AddWithValue("@discontinued", newProduct.Discontinued);

            return addProductCommand;
        }

        private static SqlParameter ParameterAddValueOrNull(string parametername, object value)
        {
            var parameter = new SqlParameter(parametername,value);
            if (value == null)
            {
                parameter.Value = DBNull.Value;
            }

            return parameter;
        }

        private class Product
        {
            public string Name { get; set; }
            public int? SupplierId { get; set; }
            public int? CategoryId { get; set; }
            public string QuantityPerUnit { get; set; }
            public decimal UnitPrice { get; set; }
            public int UnitsInStock { get; set; }
            public int UnitsInOrder { get; set; }
            public int ReorderLevel { get; set; }
            public bool Discontinued { get; set; }

            /// <summary>
            /// Won't allow creating of a product without name which is NOT NULL in DB
            /// </summary>
            /// <param name="name"></param>
            public Product(string name)
            {
                this.Name = name;
            }
        }
    }
}
