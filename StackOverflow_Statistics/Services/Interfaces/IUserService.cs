using StackOverflow_Statistics.Common.Enums;
using StackOverflow_Statistics.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services.Interfaces
{
    public interface IUserService
    {
        int GetUsersWithCommentCount();
        int GetUsersCount();
        Task<IEnumerable<UsersCommentsCountDto>> GetUserComments(int skip, int take, UserCommentOrderEnum order);
    }
}
