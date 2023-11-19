using System.Linq;

namespace Courses4All.Models
{
    public class GroupCategoryItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IGrouping<int,CategoryItemDetailViewModel> Items { get; set; }   
    }
}
