using System;
using System.ComponentModel.DataAnnotations;

namespace Courses4All.Data.Entities
{
    public class CategoryItem
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30,MinimumLength = 10)]
        public string Title {  get; set; }
        //Foreign key from Category
        public DateTime DateTimeItemReleased { get; set; }  
        

        //foreign key
        public int CategoryId { get; set; }
        public int MediaTypeId {  get; set; }
    }
}
