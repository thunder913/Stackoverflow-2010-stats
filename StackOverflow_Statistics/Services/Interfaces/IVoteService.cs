﻿using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services.Interfaces
{
    public interface IVoteService
    {
        Task<int> GetVotesCountAsync();
    }
}
