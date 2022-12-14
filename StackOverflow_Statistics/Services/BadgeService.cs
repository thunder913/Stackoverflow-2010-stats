using Microsoft.EntityFrameworkCore;
using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services
{
    internal class BadgeService : IBadgeService
    {
        private readonly ApplicationDbContext DbContext;
        public BadgeService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public async Task<int> GetBadgesCountAsync()
        {
            return await this.DbContext.Badges.CountAsync();
        }

        public async Task<long> GetUsersWithBadgesAsync()
        {
            return await this.DbContext.Badges
                .GroupBy(b => b.UserId)
                .LongCountAsync();
        }
    }
}
