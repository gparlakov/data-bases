using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.Entity.Migrations;
using System.Data;

namespace Bookmarks.Models
{
    public static class BookmarsDAO
    {
        public static void ImportXml(XmlNodeList nodes)
        {
            var dbContex = new BookmarksEntities1 ();            

            foreach (XmlNode node in nodes)
            {
                CheckNode(node);
                
                var title = node["title"].GetNodeText();

                var newBookmark = new Bookmark
                {
                    Url = node["url"].GetNodeText(),
                    Title = title,
                    Notes = node["notes"].GetNodeText(),
                    User = GetOrCreateUser(node["username"].GetNodeText(), dbContex),
                    Tags = GetOrCreateTags(node["tags"].GetNodeText(), title, dbContex)
                };

                dbContex.Bookmarks.Add(newBookmark);
                dbContex.SaveChanges();
            }
        }

        private static ICollection<Tag> GetOrCreateTags(string tags, string title, BookmarksEntities1 dbContex)
        {
            var allText = tags + "|" + title;

            var allTags = allText.Split(new char[] { '|', ',', ' ', '.', '!', '?', '\'', '\t', ';' },
                StringSplitOptions.RemoveEmptyEntries);

            var uniqueTags = GetUniqueTags(allTags);

            var tagsInContext = new List<Tag>();

            foreach (var tag in uniqueTags)
            {
                tagsInContext.Add(GetOrCreateTag(tag, dbContex));                
            }


            return tagsInContext;        
        }

        private static Tag GetOrCreateTag(string tag, BookmarksEntities1  dbContex)
        {
            var oldTag = dbContex.Tags.FirstOrDefault(t => t.Name == tag);

            if (oldTag != null)
            {
                return oldTag;
            }

            var newTag = new Tag { Name = tag };

            dbContex.Tags.Add(newTag);
            //dbContex.SaveChanges();

            return newTag;
        }

        private static HashSet<string> GetUniqueTags(string[] allTags)
        {
            var uniqueTags = new HashSet<string>();
            foreach (var tag in allTags)
            {
                var tagLower = tag.ToLower();
                if (!uniqueTags.Contains(tagLower))
                {
                    uniqueTags.Add(tagLower);
                }
            }

            return uniqueTags;
        }

        private static User GetOrCreateUser(string username, BookmarksEntities1  dbContex)
        {
            var oldUser = dbContex.Users.FirstOrDefault(u => u.Username == username);

            if (oldUser != null)
            {
                return oldUser;
            }

            var newUser = new User { Username = username };

            dbContex.Users.Add(newUser);            

            return newUser;
        }

        private static void CheckNode(XmlNode node)
        {
            if (node["username"] == null || node["title"] == null || node["url"] == null)
            {
                throw new ArgumentNullException("The xml sent to database has username, title or url null!");
            }
        }

        public static string GetNodeText(this XmlNode node)
        {
            string result = null;

            if (node != null)
            {
                result = node.InnerText;
            }

            return result;
        }
    }
}
