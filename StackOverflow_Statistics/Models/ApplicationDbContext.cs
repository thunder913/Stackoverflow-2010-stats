using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace StackOverflow_Statistics.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Badge>? Badges { get; set; }
        public virtual DbSet<Comment>? Comments { get; set; }
        public virtual DbSet<LinkType>? LinkTypes { get; set; }
        public virtual DbSet<Post>? Posts { get; set; }
        public virtual DbSet<PostLink>? PostLinks { get; set; }
        public virtual DbSet<PostType>? PostTypes { get; set; }
        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Vote>? Votes { get; set; }
        public virtual DbSet<VoteType>? VoteTypes { get; set; }
    }
}
