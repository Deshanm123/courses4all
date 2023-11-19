using Courses4All.Areas.Admin.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Courses4All.Comparers
{
    public class CompareUsers : IEqualityComparer<UserViewModel>
    {
        public bool Equals(UserViewModel x, UserViewModel y)
        {
            if (y == null) return false;
            if (x.Id == y.Id) return true;
            return false; 
        }

        public int GetHashCode([DisallowNull] UserViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
