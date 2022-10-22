using Microsoft.EntityFrameworkCore;
using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext DbContext;
        public CommentService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        
        public async Task<int> GetCommentsCountAsync()
        {
            return await DbContext.Comments.CountAsync();
        }
    }
}
