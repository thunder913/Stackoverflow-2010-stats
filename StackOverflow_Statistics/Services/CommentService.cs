using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System.Linq;

namespace StackOverflow_Statistics.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext DbContext;
        public CommentService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        
        public int GetCommentsCount()
        {
            return DbContext.Comments.Count();
        }
    }
}
