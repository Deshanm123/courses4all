using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses4All.Data.Entities
{
    public class CategoryItem
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30,MinimumLength = 10)]
        public string Title {  get; set; }
        public string Description { get; set; }
        //Foreign key from Category
        public DateTime DateTimeItemReleased { get; set; }
        [NotMapped]
        public virtual ICollection<SelectListItem> MediaTypes { get; set; }
        //foreign key
        public int CategoryId { get; set; }
        public int MediaTypeId {  get; set; }

        [NotMapped]
        public int ContentId { get; set; }
    }
}
