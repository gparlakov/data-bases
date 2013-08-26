using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BookmarksImporter
{
    internal class Importer
    {
        const string FileName = @"..\..\bookmarks.xml";

        static void Main()
        {
            var bookmarksDoc = new XmlDocument();
            bookmarksDoc.Load(FileName);

            var bookmarks = bookmarksDoc.DocumentElement.ChildNodes;
            Bookmarks.Models.BookmarsDAO.ImportXml(bookmarks);            
        }
    }
}
