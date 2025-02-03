using System.ComponentModel.DataAnnotations;
using ContentManagmentSystem.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public List<BlogPost> BlogPosts { get; set; }
   
}

