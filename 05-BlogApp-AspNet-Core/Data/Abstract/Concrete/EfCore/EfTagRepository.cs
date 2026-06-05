using BlogApp.Web.Data.Abstract; 
using BlogApp.Web.Entity;        

namespace BlogApp.Web.Data.Concrete.EfCore
{
    public class EfCoreTagRepository : ITagRepository
    {
        private readonly BlogContext _context;  

        public EfCoreTagRepository(BlogContext context) // İSİM DÜZELTİLDİ: Sınıf ismiyle aynı yapıldı
        {
            _context = context;
        }

        public IQueryable<Tag> Tags => _context.Tags;

        public void CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }
    }
}