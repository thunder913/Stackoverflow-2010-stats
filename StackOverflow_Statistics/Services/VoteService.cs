using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System;
using System.Linq;

namespace StackOverflow_Statistics.Services
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext DbContext;
        public VoteService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        
        public int GetVotesCount()
        {
            return DbContext.Votes.Count();
        }
    }
}
