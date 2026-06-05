using BlogApp.Web.Entity;
namespace BlogApp.Web.Data.Abstract
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        void CreatePost(Post post);
         
    }
}