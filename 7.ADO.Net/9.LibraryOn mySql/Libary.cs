using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;


namespace LibraryOn_mySql
{
    /// <summary>
    /// Library using my sql - there is a script file in the project directory to create a data base 
    /// </summary>
    internal class Libary
    {
        const string Server = "localhost";
        const string Database = "Library";
        const string User = "root";
        const string Password = "student";

        static void Main()
        {
            var mySQLLibraryDb = new MySqlConnection("Server=" + Server + ";Database=" + Database +
                ";Uid=" + User + ";Pwd=" + Password + ";");
            mySQLLibraryDb.Open();
            using (mySQLLibraryDb)
            {
                //WriteBook("Blown by the Wind", "O'Hara", "00005t-rttt", mySQLLibraryDb);
                //WriteBook("So long and thanks for all the Fish", "Adams", "132132-1231321", mySQLLibraryDb);
                //WriteBook("Almost Harmless", "Adams", "1584-1519", mySQLLibraryDb);
                //WriteBook("Amber Chronicles", "Zelazny", "999-999", mySQLLibraryDb);
                //WriteBook("Dying Inside", "Silverberg", "1595454-454", mySQLLibraryDb);
                //WriteBook("Guards!Guards!", "Pratchett", "121223", mySQLLibraryDb);
                //WriteBook("Clay feet", "Pratchett", "115195478", mySQLLibraryDb);

                Console.WriteLine("All Books");
                PrintAllBooks(mySQLLibraryDb);

                Console.WriteLine("\nFindBook(\"l\")");
                FindBook("l", mySQLLibraryDb);
                Console.WriteLine("\nFindBook(\"l'; INSERT INTO books(Title,Author,ISBN) VALUES('sql injection','hacker', 'hackerhacker')");
                FindBook("l'; INSERT INTO books(Title,Author,ISBN) VALUES('sql injection','hacker', 'hackerhacker') #", mySQLLibraryDb);

                Console.WriteLine("Print All books");
                PrintAllBooks(mySQLLibraryDb);

                Console.WriteLine("\nFindBook(\"fish\")");
                FindBook("fish", mySQLLibraryDb);

                PrintAllBooks(mySQLLibraryDb);
            }

        }

        private static void FindBook(string bookTitle, MySqlConnection mySQLLibraryDb)
        {
            // allows injection
            // var findBooks = new MySqlCommand("Select * FROM books WHERE title like '%@"+
            //  bookTitle + "%'", mySQLLibraryDb); 

            var findBooks = new MySqlCommand("Select * FROM books WHERE title LIKE Concat('%',@searchStr,'%')", mySQLLibraryDb);
            findBooks.Parameters.AddWithValue("@searchStr", bookTitle);                       

            var reader =  findBooks.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    var id = reader["Id"];
                    var title = reader["Title"];
                    var author = reader["Author"];

                    Console.WriteLine("{0,-2} : {1,-35} by {2} ", id, title, author);
                } 
            }          
            
        }

        private static void WriteBook(string title, string author, string ISBN, MySqlConnection mySqlConnection)
        {
            var writeCommand = new MySqlCommand("INSERT INTO books(Title,Author,ISBN) VALUES(@title,@author,@ISBN)", mySqlConnection);
            writeCommand.Parameters.AddWithValue("@title", title);
            writeCommand.Parameters.AddWithValue("@author", author);
            writeCommand.Parameters.AddWithValue("@ISBN", ISBN);
            writeCommand.ExecuteNonQuery();
        
        }

        private static void PrintAllBooks(MySqlConnection sqlConnection)
        {

            var readCommand = new MySqlCommand("SELECT * FROM Books", sqlConnection);
            var reader = readCommand.ExecuteReader();

            var result = new StringBuilder();

            using (reader)
            {
                while (reader.Read())
                {
                    var id = reader[0];
                    var title = reader["Title"];
                    var author = reader["Author"];
                    var ISBN = reader["ISBN"];

                    result.AppendFormat("{2,2}: {0,20} by {1,10} ISBN:{3}\n", title, author, id, ISBN);
                } 
            }

            Console.WriteLine(result.ToString());
        }
    }
}
