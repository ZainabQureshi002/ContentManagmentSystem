using System.ComponentModel.DataAnnotations;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public List<BlogPost> BlogPosts { get; set; }
    public List<PortfolioItem> PortfolioItems { get; set; }
}

