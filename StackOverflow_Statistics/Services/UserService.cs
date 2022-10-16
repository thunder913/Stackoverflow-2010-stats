using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System.Linq;

namespace StackOverflow_Statistics.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext DbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        
        public int GetUsersCount()
        {
            return DbContext.Users.Count();
        }
    }
}
