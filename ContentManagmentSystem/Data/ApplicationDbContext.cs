
using Microsoft.EntityFrameworkCore;

using ContentManagmentSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Replace with your actual namespace

namespace DependecyInjection.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

         public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

       
    }
}
