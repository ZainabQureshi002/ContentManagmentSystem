﻿using ContentManagmentSystem.Models;
using ContentManagmentSystem.Models.ViewModel;
using DependecyInjection.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;


namespace ContentManagmentSystem.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogPostsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // Index action: List all BlogPosts
        public async Task<IActionResult> Index()
        {
            var blogPosts = _context.BlogPosts.Include(b => b.Category);
            return View(await blogPosts.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var blogPost = await _context.BlogPosts
                .Include(b => b.Comments)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
            var postViewModel = new PostViewModel
            {
                BlogPost = blogPost,
                Categories = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                       Value = blogPost.CategoryId.ToString(),
                      Text = blogPost.Category.Name
                    }

                },
                comments = blogPost.Comments.ToList(),
                NewComment = new Comment()
            };

            return View(postViewModel);
        }
        // Create action: Show the create form
        public IActionResult Create()
        {
            PostViewModel postViewModel = new PostViewModel
            {

                Categories = _context.Categories.Select(x =>
                new SelectListItem { Text = x.Name, Value = x.Id.ToString() }
            ).ToList(),
                BlogPost = new BlogPost()

            };
            return View(postViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<IActionResult> CreateComment(int blogPostId, string userName, string content)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(content))
            {
                return BadRequest();
            }

            var comment = new Comment
            {
                BlogPostId = blogPostId,
                UserName = userName,
                Content = content,
                CreatedDate = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditComment(int id, string content)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            comment.Content = content;
            _context.Comments.Update(comment);
            _context.SaveChanges();

            return Ok(new { id = comment.Id, content = comment.Content }); // Return a 200 OK response
        }


      [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteComment(int commentId)
{
    var comment = await _context.Comments.FindAsync(commentId);
    
    if (comment == null)
    {
        return NotFound(new { message = "Comment not found." });
    }

    _context.Comments.Remove(comment);
    await _context.SaveChangesAsync();

    return Ok(new { message = "Comment deleted successfully." });
}



        // Create POST action: Save the new BlogPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload if ImageFile is provided

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                string filePath = Path.Combine(wwwRootPath, "uploads", fileName);

                // Ensure the uploads directory exists
                string uploadsDirectory = Path.Combine(wwwRootPath, "uploads");
                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }

                // Save the file to the uploads folder
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                // Save the picture URL to the database
                model.BlogPost.PictureUrl = $"/uploads/{fileName}";

                // Set the created date to the current date
                model.BlogPost.CreatedDate = DateTime.Now;

                // Add the blog post to the context and save changes
                _context.BlogPosts.Add(model.BlogPost);
                await _context.SaveChangesAsync();

                // Redirect to the index page
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts.FindAsync(id);

            PostViewModel postViewModel = new PostViewModel
            {
                BlogPost = blogPost,
                Categories = _context.Categories.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),

            };
            return View(postViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + "_" + Path.GetExtension(model.Image.FileName);
                string filePath = Path.Combine(wwwRootPath, "uploads", fileName);

                string uploadsDirectory = Path.Combine(wwwRootPath, "uploads");
                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }
                if (!string.IsNullOrEmpty(model.BlogPost.PictureUrl))
                {
                    string oldFilePath = Path.Combine(wwwRootPath, model.BlogPost.PictureUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                model.BlogPost.PictureUrl = $"/uploads/{fileName}";

                _context.Update(model.BlogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var blogpost = await _context.BlogPosts.FindAsync(id);

            return View(blogpost);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogpost = await _context.BlogPosts.FindAsync(id);
            if (blogpost == null)
            {
                return NotFound(); 
            }
            if (!string.IsNullOrEmpty(blogpost.PictureUrl))
            {
                string wwwRootPath= _webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, blogpost.PictureUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _context.BlogPosts.Remove(blogpost);
                await _context.SaveChangesAsync();

            }
                return RedirectToAction(nameof(Index));
        }
    }
}
