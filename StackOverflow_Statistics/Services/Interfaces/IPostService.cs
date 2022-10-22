using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services.Interfaces
{
    public interface IPostService
    {
        Task<int> GetPostsCountAsync();
    }
}
