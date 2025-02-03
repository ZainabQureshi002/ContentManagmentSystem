using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContentManagmentSystem.Models.ViewModel
{
    public class PostViewModel
    {
        [NotMapped]
        public IFormFile Image { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
