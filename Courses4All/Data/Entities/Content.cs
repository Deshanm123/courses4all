using System.ComponentModel.DataAnnotations;

namespace Courses4All.Data.Entities
{
    public class Content
    {
        public int Id { get; set; }
        [Required]
        public string Title {  get; set; }
        [Required]
        public string HtmlContent { get;set; }
        public string VideoLink { get; set; }
        //not vcategory items since its one to one relationship
        public CategoryItem CategoryItem { get; set; }
    }
}
