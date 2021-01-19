using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VernumBlog.Data;
using VernumBlog.Models;

namespace VernumBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VernumBlogContext _db;
        private readonly List<Category> _categories;

        public HomeController(ILogger<HomeController> logger, VernumBlogContext db)
        {
            _logger = logger;
            _db = db;
            _categories = _db.Categories.ToList();
        }

        public IActionResult Index()
        {
            // Setting ViewBag for dropdownlist in _Layout.cshtml 
            // There must be better way to do this
            ViewBag.Categories = _categories; 

            List<Post> posts = _db.Posts.Take(5).OrderByDescending(p => p.creationTime).ToList();
            foreach (var post in posts)
            {
                post.user = _db.Users.Find(post.userId);
                post.category = _db.Categories.Find(post.categoryId);
            }

            return View(posts);
        }

        public IActionResult Post(int id)
        {
            ViewBag.Categories = _categories;

            Post post = _db.Posts.Find(id);
            post.user = _db.Users.Find(post.userId);

            // /img/ is added to match the folder structure of this application
            post.imagePath = "/img/" + post.imagePath;
            return View(post);
        }

        public IActionResult Category(int categoryId)
        {
            ViewBag.Categories = _categories;

            // Populating the current category with its posts
            Category category = _db.Categories.Find(categoryId);
            List<Post> posts = _db.Posts
                .Where(p => p.categoryId == categoryId)
                .ToList();

            // Assigning users of posts
            posts.ForEach(p => p.user = _db.Users.Find(p.userId));
            category.Posts = posts;

            if (category == null)
            {
                return NotFound();
            }

            // /img/ is added to match the folder structure of this application
            category.imagePath = "/img/" + category.imagePath;
            return View(category);
        }

        public IActionResult Contact()
        {
            ViewBag.Categories = _categories;
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Categories = _categories;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
