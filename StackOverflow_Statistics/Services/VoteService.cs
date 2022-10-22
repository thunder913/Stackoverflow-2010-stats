using Microsoft.EntityFrameworkCore;
using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext DbContext;
        public VoteService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        
        public async Task<int> GetVotesCountAsync()
        {
            return await DbContext.Votes.CountAsync();
        }
    }
}
