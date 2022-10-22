using StackOverflow_Statistics.Dtos;
using StackOverflow_Statistics.Models;
using System.Collections.Generic;

namespace StackOverflow_Statistics.Common
{
    public class UserComparer : IEqualityComparer<UsersCommentsCountDto>
    {
        public bool Equals(UsersCommentsCountDto x, UsersCommentsCountDto y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(UsersCommentsCountDto obj)
        {
            return obj.GetHashCode();
        }
    }
}
