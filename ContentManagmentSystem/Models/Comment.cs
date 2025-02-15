﻿using ContentManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    public int Id { get; set; }

    [Required]
    public string UserName { get; set; }
    public string Content { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    //[Required]
    //[ForeignKey("User")]
    //public ApplicationUser User { get; set; }

    [Required]
    [ForeignKey("BlogPost")]
    public int BlogPostId { get; set; }
    [ValidateNever]
    public BlogPost BlogPost { get; set; }
}
