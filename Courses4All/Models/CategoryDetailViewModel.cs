using Courses4All.Data.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Courses4All.Models
{
    public class CategoryDetailViewModel
    {
        public IEnumerable<GroupCategoryItemViewModel> GroupCategoriesByCategory { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
