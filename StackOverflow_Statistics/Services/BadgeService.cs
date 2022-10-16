using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System.Linq;

namespace StackOverflow_Statistics.Services
{
    internal class BadgeService : IBadgeService
    {
        private readonly ApplicationDbContext DbContext;
        public BadgeService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public int GetBadgesCount()
        {
            return this.DbContext.Badges.Count();
        }
    }
}
