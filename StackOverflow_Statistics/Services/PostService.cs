using Microsoft.EntityFrameworkCore;
using StackOverflow_Statistics.Dtos;
using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System.Collections.Generic;
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

        public async Task<IEnumerable<MostViewedPostsDto>> GetMostViewedPosts(int skip, int take)
        {
            var posts = await DbContext.Posts
                .Where(p => p.OwnerUserId != 0)
                .OrderByDescending(p => p.ViewCount)
                .Skip(skip)
                .Take(take)
                .Select(p => new MostViewedPostsDto()
                {
                    AcceptedAnswer = p.AcceptedAnswerId,
                    CreationDate = p.CreationDate,
                    CreatorId = p.OwnerUserId,
                    Id = p.Id,
                    Views = p.ViewCount
                }).ToListAsync();

            var creatorIds = posts.Select(p => p.CreatorId);
            
            var creators = await DbContext.Users
                .Where(u => creatorIds.Contains(u.Id))
                .Select(u => new MostViewedPostsDto()
                {
                    Id = u.Id,
                    Creator = u.DisplayName,
                }).ToListAsync();

            var position = skip + 1;
            
            var result = posts.Join(creators, x => x.CreatorId, y => y.Id, (x, y) => new MostViewedPostsDto()
            {
                Position = position++,
                AcceptedAnswer = x.AcceptedAnswer,
                CreatorId = x.CreatorId,
                Id = x.Id,
                Views = x.Views,
                Creator = y.Creator,
                CreationDate = x.CreationDate,
            });

            return result;
        }

        public async Task<PostWithAnswerDto> GetPostAndAnswerById(long id, long acceptedAnswerId)
        {
            var acceptedAnswer = await DbContext.Posts
                .Where(p => p.Id == acceptedAnswerId)
                .Select(p => p.Body)
                .FirstOrDefaultAsync();

            var post = await DbContext.Posts
                .Where(p => p.Id == id)
                .Select(p => new PostWithAnswerDto()
                {
                    AnswerString = acceptedAnswer,
                    PostString = p.Body,
                })
                .FirstOrDefaultAsync();

            return post;
        }
    }
}
