using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System.Linq;

namespace StackOverflow_Statistics.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext DbContext;
        public PostService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        
        public int GetPostsCount()
        {
            return DbContext.Posts.Count();
        }
    }
}
