using StackOverflow_Statistics.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services.Interfaces
{
    public interface IPostService
    {
        Task<int> GetPostsCountAsync();
        Task<IEnumerable<MostViewedPostsDto>> GetMostViewedPostsAsync(int skip, int take);
        Task<PostWithAnswerDto> GetPostAndAnswerByIdAsync(long id, long acceptedAnswerId);
    }
}
