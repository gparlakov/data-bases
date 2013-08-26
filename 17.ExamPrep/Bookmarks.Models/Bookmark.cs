//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bookmarks.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bookmark
    {
        public Bookmark()
        {
            this.Tags = new HashSet<Tag>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
    
        public virtual User User { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
