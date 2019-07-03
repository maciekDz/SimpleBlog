using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.ViewModels
{
    public class SidebarTag
    {
        public int TagId { get;private set; }

        public string Name { get; private set; }
        public string Slug { get; private set; }
        public int PostCount { get; private set; }

        public SidebarTag(int id, string name, string slug, int postCount)
        {
            TagId = id;
            Name = name;
            Slug = slug;
            PostCount = postCount;
        }
    }
    
    public class LayoutSidebar
    {
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<SidebarTag> Tags { get; set; }
    }
}