using ContentManagmentSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PortfolioItem
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Link { get; set; }
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
