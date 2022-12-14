using StackOverflow_Statistics.Common.Enums;
using StackOverflow_Statistics.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> GetUsersWithCommentCountAsync();
        Task<int> GetUsersCountAsync();
        Task<IEnumerable<UsersCommentsCountDto>> GetUserCommentsAsync(int skip, int take, UserCommentOrderEnum order);
        Task<IEnumerable<UsersMostBadgesDto>> GetUsersWithMostBadgesAsync(int skip, int take);
        Task<IEnumerable<UsersReputationViewsDto>> GetUsersReputationAndViewsAsync(int skip, int take, UsersViewsReputationEnum orderType);
        Task<IEnumerable<UsersPostsCountDto>> GetUsersPostsCountAsync(int skip, int take);
    }
}
