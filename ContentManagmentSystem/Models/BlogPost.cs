using ContentManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ContentManagmentSystem.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [ValidateNever]
        public string? PictureUrl { get; set; } // Stores the path to the product's image
     
        [ValidateNever]
        public string Link { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //[Required]
        //public string Status { get; set; }

        [Required]
  
        public int CategoryId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        //[Required]
        //[ForeignKey("Author")]
        //public string AuthorId { get; set; }
        //public ApplicationUser Author { get; set; }
        [ValidateNever]
        public List<Comment>? Comments { get; set; }

    }
}