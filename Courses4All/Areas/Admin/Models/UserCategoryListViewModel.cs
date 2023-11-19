using System.Collections;
using System.Collections.Generic;

namespace Courses4All.Areas.Admin.Models
{
    public class UserCategoryListViewModel
    {
        public int CategoryId { get; set; }
        public ICollection<UserViewModel> Users { get; set; }
        public ICollection<UserViewModel> UsersSelectedList { get; set; }

    }
}
