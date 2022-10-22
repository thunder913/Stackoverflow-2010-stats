using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services.Interfaces
{
    public interface ICommentService
    {
        Task<int> GetCommentsCountAsync();
    }
}
