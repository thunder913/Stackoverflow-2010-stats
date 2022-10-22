﻿using Microsoft.EntityFrameworkCore;
using StackOverflow_Statistics.Common;
using StackOverflow_Statistics.Common.Enums;
using StackOverflow_Statistics.Dtos;
using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services.Interfaces;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow_Statistics.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext DbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<IEnumerable<UsersCommentsCountDto>> GetUserCommentsAsync(int skip, int take, UserCommentOrderEnum order)
        {
            var userIdsToTake = await DbContext.Comments
                .Where(c => c.UserId != null)
                .GroupBy(c => c.UserId)
                .OrderByDescending(c => order == UserCommentOrderEnum.CommentCount ? c.Count() : c.Sum(x => x.Score))
                .Skip(skip)
                .Take(take)
                .Select(c => new UsersCommentsCountDto
                {
                    Id = c.Key,
                    Score = c.Sum(x => x.Score),
                    CommentCount = c.Count()
                })
                .ToListAsync();

            var ids = userIdsToTake.Select(x => x.Id).ToList();

            var users = await DbContext.Users
                .Where(u => ids.Contains(u.Id))
                .Select(u => new UsersCommentsCountDto()
                {
                    DisplayName = u.DisplayName,
                    Id = u.Id
                })
                .ToListAsync();

            var position = skip + 1;
            var result = userIdsToTake.Join(users, x => x.Id, y => y.Id, (x, y) => new UsersCommentsCountDto()
            {
                Position = position++,
                CommentCount = x.CommentCount,
                DisplayName = y.DisplayName,
                Id = y.Id,
                Score = x.Score
            });

            return result;
        }

        public async Task<IEnumerable<UsersMostBadgesDto>> GetUsersWithMostBadges(int skip, int take)
        {
            {
                var userIdsToTake = await DbContext.Badges
                    .GroupBy(b => b.UserId)
                    .OrderByDescending(b => b.Count())
                    .Skip(skip)
                    .Take(take)
                    .Select(b => new UsersMostBadgesDto
                    {
                        Id = b.Key,
                        BadgeCount = b.Count()
                    })
                    .ToListAsync();

                var ids = userIdsToTake.Select(x => x.Id).ToList();

                var users = await DbContext.Users
                    .Where(u => ids.Contains(u.Id))
                    .Select(u => new UsersCommentsCountDto()
                    {
                        DisplayName = u.DisplayName,
                        Id = u.Id
                    })
                    .ToListAsync();

                var position = skip + 1;
                var result = userIdsToTake.Join(users, x => x.Id, y => y.Id, (x, y) => new UsersMostBadgesDto()
                {
                    Position = position++,
                    BadgeCount = x.BadgeCount,
                    DisplayName = y.DisplayName,
                    Id = y.Id
                });

                return result;
            }
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await DbContext.Users.CountAsync();
        }

        public async Task<int> GetUsersWithCommentCountAsync()
        {
            return await DbContext.Comments
                .Where(c => c.UserId != null)
                .GroupBy(c => c.UserId)
                .CountAsync();
        }
    }
}
