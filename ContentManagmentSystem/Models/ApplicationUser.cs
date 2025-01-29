using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ContentManagmentSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

      
    }

}
