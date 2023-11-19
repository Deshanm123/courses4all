using Courses4All.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses4All.Data.Entities
{
    public class Category:IPrimaryProperties
    {
        public int Id { get; set; } 

        [Required]
        [StringLength(60 ,MinimumLength = 8)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name ="Cover Image")]
        public string CoverImageUrl { get; set; }


        //This referencing for the foreign key.
        //IEnumerable<CategoryItem> is not using due Ienumerable is readonly
        //ICollection inherits from Ienumberable and it also has add methods
        [ForeignKey("CategoryId")]
        public ICollection<CategoryItem> CategoryItems { get; set; }

        [ForeignKey("CategoryId")]
        //[ForeignKey("UserCategoryId")]
        public virtual ICollection<UserCategory> UserCategories { get; set; }

    }
}
