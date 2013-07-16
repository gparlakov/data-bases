using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _5.AllCategoryImages
{
    internal class AllCategoryImages
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

                var getImagesCommand = new SqlCommand("Select Picture FROM Categories", nortwindDb);
                var reader = getImagesCommand.ExecuteReader();
                var counter = 1;
                
                while (reader.Read())
                {
                    var rawData = reader["Picture"];
                    var imageByte = (byte[])rawData;

                    var fileStream = File.OpenWrite("../../Images/Image" + counter + ".jpeg");
                    using (fileStream)
                    {
                        fileStream.Write(imageByte, 0, imageByte.Length);
                    }                    

                    counter++;
                }
               
            }

        }
    }
}
