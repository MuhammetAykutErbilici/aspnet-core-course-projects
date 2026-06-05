using Microsoft.AspNetCore.Mvc;
using BlogApp.Web.Data.Abstract;
using BlogApp.Web.Entity;
using BlogApp.Web.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _PostRepository;

        public PostController(IPostRepository Postrepository)
        {
            _PostRepository = Postrepository;
        }

        public async Task<IActionResult> Index(string tag)
        {
            var posts = _PostRepository.Posts;
            if (!string.IsNullOrEmpty(tag))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            return View(new PostsViewModel{Posts = await posts.ToListAsync()});
        }

        public async Task<IActionResult> Details(string url)
        {
            if (url == null)
            {
                return NotFound();
            }

            var post = await _PostRepository.Posts.Include(x => x.Tags).Include(x => x.Comments).ThenInclude(x => x.User).FirstOrDefaultAsync(p => p.Url == url);
            
            if (post == null)
            {
                return NotFound();
            }

            return View(post); // Tek bir Post nesnesi gönderiyoruz
        }
    }
}