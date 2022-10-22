using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services.Interfaces
{
    public interface IBadgeService
    {
        Task<int> GetBadgesCountAsync();
    }
}
