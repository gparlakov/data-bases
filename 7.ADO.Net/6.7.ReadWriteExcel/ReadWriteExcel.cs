using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace _6.ReadExcel
{
    internal class ReadWriteExcel
    {
        static void Main()
        {
            var excelConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;"+
                "Data Source=..\\..\\excelFile\\data.xlsx;Extended Properties=\"Excel 12.0 XML;HDR=Yes\"");
            
            excelConnection.Open();

            using (excelConnection)
            {
                PrintAll(excelConnection);

                AddNameScore(excelConnection,"Kovachev", 5);
            }
        }

        //task 8
        /// <summary>
        /// Create an Excel file with 2 columns: name and score:
        /// Write a program that reads your MS Excel file through the OLE 
        /// DB data provider and displays the name and score row by row.
        /// </summary>
        /// <param name="excelConnection"></param>
        /// <param name="name"></param>
        /// <param name="score"></param>
        private static void AddNameScore(OleDbConnection excelConnection,string name, int score)
        {
            var commandWriter = new OleDbCommand("INSERT INTO [Sheet1$] VALUES (@name, @score)", excelConnection);

            commandWriter.Parameters.AddWithValue("@name", name);
            commandWriter.Parameters.AddWithValue("@score", score);
            commandWriter.ExecuteNonQuery();
        }

        //task6
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelConnection"></param>
        private static void PrintAll(OleDbConnection excelConnection)
        {  
            var schema = excelConnection.GetSchema();

            var command = new OleDbCommand("SELECT * FROM [Sheet1$]", excelConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var name = reader[0];
                var score = reader[1];
                Console.WriteLine("{0} -> {1}", name, score);
            }            
        }
    }
}
