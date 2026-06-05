using BlogApp.Web.Entity;
namespace BlogApp.Web.Data.Abstract
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags { get; }
        void CreateTag(Tag tag);
         
    }
}