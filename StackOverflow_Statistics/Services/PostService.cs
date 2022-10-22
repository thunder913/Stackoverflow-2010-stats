using Microsoft.EntityFrameworkCore;
using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext DbContext;
        public PostService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        
        public async Task<int> GetPostsCountAsync()
        {
            return await DbContext.Posts.CountAsync();
        }
    }
}
