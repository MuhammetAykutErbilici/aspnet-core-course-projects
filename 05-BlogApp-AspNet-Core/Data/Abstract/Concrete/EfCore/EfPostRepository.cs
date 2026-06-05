using BlogApp.Web.Data.Abstract; 
using BlogApp.Web.Entity;        

namespace BlogApp.Web.Data.Concrete.EfCore
{
    public class EfCorePostRepository : IPostRepository
    {
        private readonly BlogContext _context;  

        public EfCorePostRepository(BlogContext context) // İSİM DÜZELTİLDİ: Sınıf ismiyle aynı yapıldı
        {
            _context = context;
        }

        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}