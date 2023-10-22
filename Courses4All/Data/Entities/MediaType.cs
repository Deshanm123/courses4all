using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses4All.Data.Entities
{
    public class MediaType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200,MinimumLength = 10)]

        public string Title { get;set; }

        [Required]
        public string CoverImage {  get; set; }



        [ForeignKey("MediaTypeId")]
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }

    }
}
