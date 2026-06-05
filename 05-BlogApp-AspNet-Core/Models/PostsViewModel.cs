using System.Collections.Generic;
using BlogApp.Web.Entity;

namespace BlogApp.Web.Models
{
    public class PostsViewModel
    {
        public List<Post> Posts { get; set; } = new();
    }
}