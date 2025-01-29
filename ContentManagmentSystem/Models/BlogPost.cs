using ContentManagmentSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BlogPost
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    [Required]
    public string Status { get; set; }

    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    //[Required]
    //[ForeignKey("Author")]
    //public string AuthorId { get; set; }
    //public ApplicationUser Author { get; set; }

    public ICollection<Comment> Comments { get; set; }
    
}
