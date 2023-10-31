using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses4All.Data.Entities
{
    public class Content
    {
        public int Id { get; set; }
        [Required]
        public string Title {  get; set; }
        [Required]

        [Display(Name = "HTML Content")]
        public string HtmlContent { get;set; }
        public string VideoLink { get; set; }
        
        //not category items since its one to one relationship
        public CategoryItem CategoryItem { get; set; }
        
        [NotMapped]
        public int Category_Item_Id { get; set; }

        [NotMapped]
        public int CategoryId { get;set; }
    }
}
